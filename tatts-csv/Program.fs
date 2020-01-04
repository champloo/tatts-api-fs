open System
open System.IO
open System.Text.RegularExpressions

open Argu
open TattsApi

type CLIArguments =
    | [<AltCommandLine("-d")>] Date of date:string
    | [<AltCommandLine("-o")>] OutputFile of path:string
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Date _ -> "date in yyyy-MM-dd format for which to collect data. If not specified today's date is used."
            | OutputFile _ -> "output file. If not specified a filename is automatically created with name tatts-csv_yyyy-MM-dd.txt."


// Match the pattern using a cached compiled Regex
let (|CompiledMatch|_|) pattern input =
    if input = null then None
    else
        let m = Regex.Match(input, pattern, RegexOptions.Compiled)
        if m.Success then Some (List.tail [for x in m.Groups -> x.Value])
        else None

// Renames venues containg "Pt" and "Mt" to "Port" and "Mount".
// For example "Pt Macquarie" to "Port Macquarie"
let renameVenue name =
    match name with
    | CompiledMatch @"^Pt (.*)" [n] -> "Port " + n
    | CompiledMatch @"^Mt (.*)" [n] -> "Mount " + n
    | n -> n

[<EntryPoint>]
let main argv =
    let parser = ArgumentParser.Create<CLIArguments>(programName = "tatts-csv.exe")
    let results = parser.ParseCommandLine(raiseOnUsage = true)
    
    let dateArg = results.GetResult (Date, defaultValue = "")
    let date = if (dateArg).Equals("") then DateTime.Today else Convert.ToDateTime(dateArg)
    
    let outputFile = results.GetResult (OutputFile, defaultValue = "tatts-csv_" + date.ToString("yyyy-MM-dd") + ".txt")

    let writeSet =
        seq {
            let raceday = RacingInformation.getRaceday date
            for meeting in raceday.Meetings do
                if (meeting.MeetingType = "R" && meeting.Abandoned = "N") then
                    for meetingRace in meeting.Races do
                        let race = RacingInformation.getRace date meeting.MeetingCode meetingRace.RaceNo

                        for runner in race.Meeting.Races.[0].Runners do
                            sprintf "%s,%s,%i,%i,%s,%M,%s,%s,%s,%i,%i,%s,%i" 
                                (renameVenue (race.Meeting.VenueName))
                                (race.RaceDayDate.ToString("dd/MM/yyyy"))
                                race.Meeting.Races.[0].RaceNo
                                runner.RunnerNo
                                runner.RunnerName
                                (Option.defaultValue 0m runner.Weight)
                                runner.Scratched
                                (Option.defaultValue "" runner.Rider)
                                runner.RiderChanged
                                (Option.defaultValue 0 runner.Barrier)
                                runner.Handicap
                                (Option.defaultValue "" runner.LastResult)
                                (Option.defaultValue 0 runner.Rtng)
        }
    File.WriteAllLines(outputFile, writeSet)

    0
