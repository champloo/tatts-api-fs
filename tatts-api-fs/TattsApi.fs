namespace TattsApi

open System
open System.Net
open FSharp.Data

open Util

module RacingInformation =
    [<Literal>]
    let schemaFolder = __SOURCE_DIRECTORY__ + "/schema/"

    type RacedayFeed = XmlProvider<ResolutionFolder=schemaFolder, Schema="./raceday.xsd", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.raceday.xsd">
    type MeetingFeed = XmlProvider<ResolutionFolder=schemaFolder, Schema="./meeting.xsd", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.meeting.xsd">
    type RaceFeed = XmlProvider<ResolutionFolder=schemaFolder, Schema="./race.xsd", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.race.xsd">
    type ScratchingsFeed = XmlProvider<ResolutionFolder=schemaFolder, Schema="./scratchings.xsd", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.scratchings.xsd">
    type DoubleFeed = XmlProvider<ResolutionFolder=schemaFolder, Schema="./double.xsd", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.double.xsd">
    type FieldFeed = XmlProvider<ResolutionFolder=schemaFolder, Schema="./field.xsd", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.field.xsd">

    let private buildUrl (date:DateTime) feedname =
        let baseUrl = "http://tatts.com/pagedata/racing/"
        baseUrl + date.ToString("yyyy/M/d") + "/" + feedname + ".xml"

    let private load (url:string) =
        let wc = new WebClient()
        retry defaultRetryParams (fun() -> wc.DownloadString(url))

    let private loadRaceday (date:DateTime) = load (buildUrl date "RaceDay")
    let private loadMeeting (date:DateTime) meetingId = load (buildUrl date meetingId)
    let private loadRace (date:DateTime) meetingId raceNum = load (buildUrl date (meetingId + raceNum.ToString()))
    let private loadScratchings (date:DateTime) = load (buildUrl date "scratchings")
    let private loadDouble (date:DateTime) meetingId = load (buildUrl date (meetingId + "doubtreb"))
    let private loadField (date:DateTime) meetingId = load (buildUrl date (meetingId + "Fields"))

    let getRaceday date =
        RacedayFeed.Parse(loadRaceday date)

    let getMeeting date meetingId =
        MeetingFeed.Parse(loadMeeting date meetingId)

    let getRace date meetingId (raceNum:int) =
        RaceFeed.Parse(loadRace date meetingId raceNum)

    let getScratchings date =
        ScratchingsFeed.Parse(loadScratchings date)

    let getDouble date meetingId =
        DoubleFeed.Parse(loadDouble date meetingId)

    let getField date meetingId =
        FieldFeed.Parse(loadField date meetingId)