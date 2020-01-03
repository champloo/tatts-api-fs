namespace TattsApi

open System
open System.Net
open FSharp.Data

open Util

module RacingInformation =
    type RacedayFeed = XmlProvider<"""
    <RaceDay RaceDayDate="2013-01-12T00:00:00" Year="2013" Month="1" Day="12" DayOfTheWeek="Saturday" MonthLong="January" IsCurrentDay="1" IsPresaleMeeting="0" ServerTime="2013-01-13T01:16:36.233">
      <PresaleRaceDate RaceDayDate="2013-01-13T00:00:00" Year="2013" Month="1" Day="12" DayOfTheWeek="Saturday" MonthLong="January" IsCurrentDay="1" IsPresaleMeeting="0" />
      <Meeting MeetingType="R" Abandoned="N" VenueName="Eagle Farm" SortOrder="0" HiRaceNo="8" NextRaceNo="1" MeetingCode="BR" MtgId="653525760">
        <Pool PoolType="DD" Available="Y" JPotInGross="0.0000">
          <MultiLeg LegNo="1" RaceNo="7" />
        </Pool>
        <Race RaceDisplayStatus="PAYING" RaceNo="1" RaceTime="2013-01-12T12:30:00" CloseTime="2013-01-12T12:30:43.787">
          <ResultPlace PlaceNo="1">
            <Result RunnerNo="5" PoolType="PP" Ref50Pct="N" RefScratched="N" RefAbandon="N" RefNoWinner="N" NoPlacePool="N">
              <PoolResult PoolType="PP" Dividend="2.90" />
            </Result>
          </ResultPlace>
          <FixedOdds HasFixedPrice="1" />
          <Pool PoolType="PP" Available="1" JPotInGross="0.0000" />
        </Race>
        <Race RaceDisplayStatus="PAYING" RaceNo="1" RaceTime="2013-01-12T12:30:00" CloseTime="2013-01-12T12:30:43.787"/>
      </Meeting>
      <Meeting MeetingType="R" Abandoned="N" VenueName="Eagle Farm" SortOrder="0" HiRaceNo="8" NextRaceNo="1" MeetingCode="BR" MtgId="653525760"/>
    </RaceDay>""">

    type MeetingFeed = XmlProvider<"""
    <RaceDay RaceDayDate="2013-01-12T00:00:00" Year="2013" Month="1" Day="12" DayOfTheWeek="Saturday" MonthLong="January" IsCurrentDay="1" IsPresaleMeeting="0" ServerTime="2013-01-12T17:26:31.193">
      <Meeting MeetingCode="BR" MtgId="655622144" VenueName="Eagle Farm" MtgType="R" TrackDesc="Good" TrackCond="1" TrackRatingChanged="N" TrackRating="3" WeatherChanged="N" TrackChanged="N" WeatherCond="1" WeatherDesc="Fine" MtgAbandoned="N">
        <Pool PoolType="DD" PoolDisplayStatus="PAYING" Available="Y" Abandoned="N" JPotInGross="0.00" JPotOutGross="0.00" PoolTotal="12243.00">
          <MultiLeg LegNo="1" RaceNo="7">
            <DividendResult RunnerNo="10" DivAmount="60.20" DivId="65692311" />
          </MultiLeg>
          <Dividend DivId="65692311" DivAmount="60.2000">
            <DivResult RunnerNo="10" LegNo="1" RaceNo="7" />
          </Dividend>
        </Pool>
        <MultiPool PoolType="DD" PoolDisplayStatus="PAYING" Available="Y" Abandoned="N" JPotInGross="0.00" JPotOutGross="0.00" PoolTotal="12243.00">
          <Dividend DivId="65692311" DivAmount="60.2000">
            <DivResult RunnerNo="10" LegNo="1" RaceNo="7" />
          </Dividend>
        </MultiPool>
        <Race RaceNo="1" RaceTime="2013-01-12T12:30:00" RaceName="BM 80 HANDICAP" Distance="2200" RaceDisplayStatus="PAYING" WeatherChanged="N" WeatherCond="1" WeatherDesc="Fine" TrackChanged="N" TrackCond="1" TrackDesc="Good">
          <TipsterTip TipsterId="0" Tips="2" />
          <Pool PoolType="A2" Available="Y" Abandoned="N" PoolDisplayStatus="PAYING" PoolTotal="3027.00" JPotInGross="0.00" JPotOutGross="0.00" LastCalcTime="2013-01-12T12:28:55" CalcTime="2013-01-12T12:32:57" StatDiv="0">
            <Dividend DivId="65691182" DivAmount="10.7000">
              <DivResult LegNo="1" RunnerNo="4" />
            </Dividend>
          </Pool>
          <ResultPlace PlaceNo="1">
            <Result RunnerNo="5" PoolType="PP" Ref50Pct="N" RefScratched="N" RefAbandon="N" RefNoWinner="N" NoPlacePool="N">
              <PoolResult PoolType="PP" Dividend="2.90" />
            </Result>
          </ResultPlace>
          <FixedPriceSummary>
            <FixedPrice SportId="8" LeagueId="102" MeetingId="1246" MainEventId="166449" SubEventId="1099597" Status="F" BetTypeName="Win" EnablePlaceBetting="1"/>
          </FixedPriceSummary>
          <Runner RunnerNo="1" RunnerName="ENCOURAGABLE" Barrier="7" Scratched="Y" ScratchStatus="3" />
          <Protest Protestor="1" Defendant="1" />
          <Photo PlaceGetter="1" />
        </Race>
        <Tipster TipsterId="0" TipsterName="LATE MAIL" />
      </Meeting>
    </RaceDay>
    """>

    type RaceFeed = XmlProvider<"""
    <RaceDay RaceDayDate="2013-01-12T00:00:00" Year="2013" Month="1" Day="12" DayOfTheWeek="Saturday" MonthLong="January" IsCurrentDay="1" IsPresaleMeeting="0" ServerTime="2013-01-12T12:40:11.600">
      <Meeting MeetingCode="BR" MtgId="655622144" VenueName="Eagle Farm" MtgType="R" TrackDesc="Good" TrackCond="1" TrackRating="3" WeatherCond="1" WeatherDesc="Fine" MtgAbandoned="N">
        <Pool PoolType="DD" PoolDisplayStatus="SELLING" Available="Y" Abandoned="N" JPotInGross="0.00" JPotOutGross="0.00" PoolTotal="2854.00">
          <MultiLeg LegNo="1" RaceNo="7">
            <DividendResult RunnerNo="3" DivAmount="10.60" DivId="65785769"/>
          </MultiLeg>
          <Dividend DivId="1" DivAmount="1">
            <DivResult RunnerNo="1" LegNo="1" RaceNo="1">DivResult1</DivResult>
          </Dividend>
        </Pool>
        <MultiPool PoolType="XD" PoolDisplayStatus="CLOSED" Available="Y" Abandoned="N" JPotInGross="0.00" JPotOutGross="0.00" PoolTotal="2103.00">
          <Dividend DivId="65785769" DivAmount="10.6000">
            <DivResult RunnerNo="3" LegNo="1" RaceNo="1"/>
          </Dividend>
        </MultiPool>
        <ValidRace RaceNo="1"/>
        <Race RaceNo="1" RaceTime="2013-01-12T12:30:00" RaceName="BM 80 HANDICAP" Distance="2200" RaceDisplayStatus="PAYING" WeatherChanged="N" WeatherCond="1" WeatherDesc="Fine" TrackChanged="N" TrackCond="1" TrackDesc="Good">
            <TipsterTip TipsterId="0" Tips="2">
                <Tipster TipsterName="LATE MAIL"/>
            </TipsterTip>
            <Pool PoolType="A2" Available="Y" Abandoned="N" PoolDisplayStatus="PAYING" PoolTotal="3027.00" JPotInGross="0.00" JPotOutGross="0.00" LastCalcTime="2013-01-12T12:28:55" CalcTime="2013-01-12T12:32:57" StatDiv="0">
                <Dividend DivId="65691182" DivAmount="10.7000">
                <DivResult LegNo="1" RunnerNo="4" />
                </Dividend>
            </Pool>
            <ResultPlace PlaceNo="1">
                <Result RunnerNo="5" PoolType="PP" Ref50Pct="N" RefScratched="N" RefAbandon="N" RefNoWinner="N" NoPlacePool="N">
                <PoolResult PoolType="PP" Dividend="2.90" />
                </Result>
            </ResultPlace>
            <FixedPriceSummary>
                <FixedPrice SportId="8" LeagueId="102" MeetingId="1246" MainEventId="166449" SubEventId="1099597" Status="F" BetTypeName="Win" EnablePlaceBetting="1"/>
            </FixedPriceSummary>
            <Runner RunnerNo="1" RunnerName="MAGNUM" Scratched="N" ScratchStatus="1" Rider="A PATTILLO" RiderChanged="N" Barrier="1" Handicap="0" Weight="57.5" LastResult="122" Rtng="100">
                <WinOdds Odds="2.00" Lastodds="1.90" LastCalcTime="2013-01-19T13:26:43" CalcTime="2013-01-19T13:30:47" Short="N"/>
                <PlaceOdds Odds="1.20" Lastodds="1.10" Short="N"/>
                <FixedOdds OfferId="981252" RunnerNo="01" RaceNo="01" MeetingCode="QR" RaceDayDate="2013-01-19T00:00:00" WinOdds="2.2000" PlaceOdds="1.3000" RetailWinOdds="2.2000" RetailPlaceOdds="1.3000" OfferName="MAGNUM" Status="p" LateScratching="0" Deduction="0" PlaceDeduction="0">
                <Book BookStatus="F" SubEventId="1099597"/>
                </FixedOdds>
            </Runner>
            <Runner RunnerNo="1" RunnerName="MAGNUM" Scratched="N" ScratchStatus="1" Rider="A PATTILLO" RiderChanged="N" Barrier="1" Handicap="0" Weight="57.5" LastResult="122" Rtng="100"/>
            <SubFavCandidate RunnerNo="1"/>
            <Protest Protestor="1" Defendant="1" />
            <Photo PlaceGetter="1" />
        </Race>
      </Meeting>
    </RaceDay>
    """>

    type ScratchingsFeed = XmlProvider<"""
    <RaceDay RaceDayDate="2013-01-19T00:00:00" Year="2013" Month="1" Day="19" DayOfTheWeek="Saturday" MonthLong="January" IsCurrentDay="1" IsPresaleMeeting="0" ServerTime="2013-01-20T00:55:36.030">
        <Meeting MeetingCode="BR" MtgId="653787136" VenueName="Eagle Farm" MtgType="R" MtgAbandoned="N" WeatherChanged="N" WeatherCond="1" WeatherDesc="Fine" TrackChanged="N" TrackCond="1" TrackDesc="Good" TrackRating="3" SortOrder="0">
        <Race RaceNo="2" Racetime="2013-01-19T13:00:00" Racename="NMW HANDICAP" RaceDisplayStatus="PAYING" WeatherChanged="N" WeatherCond="1" WeatherDesc="Fine" TrackChanged="N" TrackCond="1" TrackDesc="Good" TrackRating="3" ScratchingsFinal="Y">
            <Runner RunnerNo="8" RunnerName="TY SEEKER" Scratched="Y" ScratchStatus="1"/>
        </Race>
        </Meeting>
    </RaceDay>
    """>
    type DoubleFeed = XmlProvider<"""
    <RaceDay RaceDayDate="2013-01-19T00:00:00" Year="2013" Month="1" Day="19" DayOfTheWeek="Saturday" MonthLong="January" IsCurrentDay="1" IsPresaleMeeting="0" ServerTime="2013-01-19T16:52:23.290">
      <Meeting MeetingCode="QR" MtgId="653787904" VenueName="Gold Coast" MtgType="R" TrackDesc="Good" TrackCond="1" TrackRating="3" WeatherCond="1" WeatherDesc="Fine" MtgAbandoned="N">
        <Pool PoolType="DD" PoolDisplayStatus="PAYING" Available="Y" Abandoned="N" JPotInGross="0.00" JPotOutGross="0.00" PoolTotal="4024.00">
          <MultiLeg LegNo="1" RaceNo="5">
            <DividendResult RunnerNo="3" DivAmount="125.90" DivId="65786378"/>
          </MultiLeg>
        </Pool>
        <MultiPool PoolType="DD" PoolDisplayStatus="PAYING" Available="Y" Abandoned="N" JPotInGross="0.00" JPotOutGross="0.00" PoolTotal="4024.00">
          <Dividend DivId="65786378" DivAmount="125.9000">
            <DivResult RunnerNo="3" LegNo="1" RaceNo="5"/>
          </Dividend>
        </MultiPool>
        <MultiLegPoolOdds PoolType="DD">
          <OddsML Combination="1" RunnerNo="1" Odds="419430.3500" Shortening="0" LastLegOCF="6" LegWinner1="3" LegWinner2="0" LegWinner3="0"/>
        </MultiLegPoolOdds>
        <Race RaceNo="1" Racetime="2013-01-19T13:25:00" Racename="CLASS 3 PLATE" Distance="2200" SubFav="1" RaceDisplayStatus="PAYING" WeatherCond="1" WeatherDesc="Fine" TrackCond="1" TrackDesc="Good">
          <TipsterTip TipsterId="0" Tips="1"/>
          <Pool PoolType="EX" Available="Y" Abandoned="N" PoolDisplayStatus="PAYING" PoolTotal="2149.00" JPotInGross="0.00" JPotOutGross="0.00" LastCalcTime="2013-01-19T13:26:43" CalcTime="2013-01-19T13:30:47" StatDiv="0">
            <Dividend DivId="65785625" DivAmount="13.7000">
              <DivResult LegNo="1" RunnerNo="3"/>
            </Dividend>
          </Pool>
          <ResultPlace PlaceNo="1">
            <Result RunnerNo="3" PoolType="PP" Ref50Pct="N" RefScratched="N" RefAbandon="N" RefNoWinner="N" NoPlacePool="N">
              <PoolResult PoolType="PP" Dividend="2.00"/>
            </Result>
          </ResultPlace>
          <Runner RunnerNo="1" RunnerName="MAGNUM" Scratched="Y" ScratchStatus="3">
            <WinOdds Odds="2.00" Lastodds="1.90" LastCalcTime="2013-01-19T13:26:43" CalcTime="2013-01-19T13:30:47" Short="N"/>
            <PlaceOdds Odds="1.20" Lastodds="1.10" Short="N"/>
          </Runner>
          <SubFavCandidate RunnerNo="1"/>
        </Race>
      </Meeting>
    </RaceDay>
    """>

    type FieldFeed = XmlProvider<"""
    <RaceDay RaceDayDate="2013-01-19T00:00:00" Year="2013" Month="1" Day="19" DayOfTheWeek="Saturday" MonthLong="January" IsCurrentDay="1" IsPresaleMeeting="0" ServerTime="2013-01-19T16:52:25.180">
      <Meeting MeetingCode="QR" MtgId="653787904" VenueName="Gold Coast" MtgType="R" TrackDesc="Good" TrackCond="1" TrackRatingChanged="0" TrackRating="3" WeatherCond="1" WeatherDesc="Fine" MtgAbandoned="N">
        <Pool PoolType="DD" PoolDisplayStatus="PAYING" Available="Y" Abandoned="N" JPotInGross="0.00" JPotOutGross="0.00" PoolTotal="4024.00">
          <MultiLeg LegNo="1" RaceNo="5">
            <DividendResult RunnerNo="3" DivAmount="125.90" DivId="65786378"/>
          </MultiLeg>
        </Pool>
        <Race RaceNo="1" RaceTime="2013-01-19T13:25:00" RaceName="CLASS 3 PLATE" Distance="2200" SubFav="1" RaceDisplayStatus="PAYING" WeatherCond="1" WeatherDesc="Fine" TrackCond="1" TrackDesc="Good">
          <TipsterTip TipsterId="0" Tips="1"/>
          <Pool PoolType="EX" Available="Y" Abandoned="N" PoolDisplayStatus="PAYING" PoolTotal="2149.00" JPotInGross="0.00" JPotOutGross="0.00" LastCalcTime="2013-01-19T13:26:43" CalcTime="2013-01-19T13:30:47" StatDiv="0">
            <Dividend DivId="65785625" DivAmount="13.7000">
              <DivResult LegNo="1" RunnerNo="3"/>
            </Dividend>
          </Pool>
          <ResultPlace PlaceNo="1">
            <Result RunnerNo="3" PoolType="PP" Ref50Pct="N" RefScratched="N" RefAbandon="N" RefNoWinner="N" NoPlacePool="N">
              <PoolResult PoolType="PP" Dividend="2.00"/>
            </Result>
          </ResultPlace>
          <Runner RunnerNo="1" RunnerName="MAGNUM" Weight="57.5" Barrier="1" Handicap="0" Scratched="Y" ScratchStatus="1">
            <WinOdds Odds="2.00" Lastodds="1.90" LastCalcTime="2013-01-19T13:26:43" CalcTime="2013-01-19T13:30:47" Short="N"/>
          </Runner>
          <Protest Protestor="1" Defendant="1" />
          <Photo PlaceGetter="1" />
        </Race>
        <Tipster TipsterId="0" TipsterName="LATE MAIL"/>
      </Meeting>
    </RaceDay>
    """>

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