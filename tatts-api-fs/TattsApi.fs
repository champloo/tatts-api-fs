namespace TattsApi

open System
open System.Net
open FSharp.Data

open Util

module RacingInformation =
    type RacedayFeed = XmlProvider<"schema/raceday.xml", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.raceday.xml">
    type MeetingFeed = XmlProvider<"schema/meeting.xml", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.meeting.xml">
    type RaceFeed = XmlProvider<"schema/race.xml", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.race.xml">
    type ScratchingsFeed = XmlProvider<"schema/scratchings.xml", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.scratchings.xml">
    type DoubleFeed = XmlProvider<"schema/double.xml", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.double.xml">
    type FieldFeed = XmlProvider<"schema/field.xml", EmbeddedResource="tatts-api-fs, tatts_api_fs.schema.field.xml">

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