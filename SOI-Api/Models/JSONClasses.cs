using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core.Objects;
using Microsoft.Data.OData.Query.SemanticAst;
using SOI_Api.Controllers;

namespace SOI_Api.Models
{
    public class Delegation
    {
        public string guid { get; set; }
        public string name { get; set; }

        public Delegation(apiGetDelegationsListByGameID_Result dbDelegation)
        {
            guid = dbDelegation.dlgn_guid;
            name = dbDelegation.dlgn_name;
        }

        public Delegation(apiSearchAthleteByEvent_Result2 dbAthlete)
        {
            guid = dbAthlete.delg_guid;
            name = dbAthlete.delg_name;
        }

        public Delegation(apiSearchAthleteByID_Result dbAthlete)
        {
            guid = dbAthlete.delg_guid;
            name = dbAthlete.delg_name;
        }
        
    }

    public class Division
    {
        public string name { get; set; }
        public string round { get; set; }
        public string brackettype { get; set; }
        public List<Athlete> athletes { get; set; }
        public List<Team> teams { get; set; }

        public Division(apiSearchAthleteByEvent_Result2 dbAthlete)
        {
            name = dbAthlete.dvsn_name;
            round = dbAthlete.dvsn_round.ToString();
            brackettype = dbAthlete.IsBracketed.ToString();
            if (dbAthlete.EventType == "t")
            {  // teams and then athletes
                teams = new List<Team>();
            }
            else
            {  // just athletes
                athletes = new List<Athlete>();
            }            
            Add(dbAthlete);
        }

        public void Add(apiSearchAthleteByEvent_Result2 dbAthlete)
        {
            if (dbAthlete.EventType == "t")
            {
                string matchId = string.IsNullOrEmpty(dbAthlete.rslt_matchid) ? "" : dbAthlete.rslt_matchid;
                if ((teams.Count > 0) && (teams[teams.Count - 1].guid == dbAthlete.team_guid) && (teams[teams.Count - 1].matchid == matchId))
                {
                    teams[teams.Count - 1].Add(dbAthlete);
                }
                else
                {
                    teams.Add(new Team(dbAthlete));
                }
            }
            else
            {
                athletes.Add(new Athlete(dbAthlete, false));
            }
        }


    }
        
    public class EventByGameIdRootObject
    {
        public List<Sport> sports { get; set; }

        public EventByGameIdRootObject()
        {
            sports = new List<Sport>();
        }

        public void Add(apiGetEventsListByGameID_Result dbSportingEvent)
        {
            if ((sports.Count > 0) && (sports[sports.Count - 1].guid == dbSportingEvent.sprt_Code))
            {
                sports[sports.Count - 1].Add(dbSportingEvent);
            }
            else
            {
                sports.Add(new Sport(dbSportingEvent));
            }
        }
    }

    public class DelegationByGameIdRootObject
    {
        public List<Delegation> delegations { get; set; }

        public DelegationByGameIdRootObject()
        {
            delegations = new List<Delegation>();
        }

        public void Add(apiGetDelegationsListByGameID_Result dbDelegation)
        {
            delegations.Add(new Delegation(dbDelegation));
        }
    }

    public class Name
    {
        public string first { get; set; }
        public string last { get; set; }

        public Name(apiSearchAthleteByEvent_Result2 dbAthlete)
        {
            first = dbAthlete.athl_first;
            last = dbAthlete.athl_last;
        }

        public Name(apiSearchAthleteByID_Result dbAthlete)
        {
            first = dbAthlete.athl_first;
            last = dbAthlete.athl_last;
        }

    }

    public class Result
    {
        public string bib { get; set; }
        public string division { get; set; }
        public string date { get; set; }
        public string score { get; set; }
        public string place { get; set; }
        public string round { get; set; }
        public string matchid { get; set; }
        public string matchstatus { get; set; }
        public string divisionstatus { get; set; }
        public string venue { get; set; }
        public bool personalbest { get; set; }

        public Result(apiSearchAthleteByID_Result dbAthlete)
        {
            bib = dbAthlete.rslt_bib;
            division = dbAthlete.rslt_division;
            divisionstatus = dbAthlete.rslt_divisionstatus;
            matchid = string.IsNullOrEmpty(dbAthlete.rslt_matchid) ? "" : dbAthlete.rslt_matchid;
            matchstatus = dbAthlete.rslt_matchstatus;
            if (dbAthlete.rslt_date.HasValue)
            {
                DateTime tmpDate = dbAthlete.rslt_date.GetValueOrDefault();
                if (tmpDate.Year == 1899)
                {
                    date = null;
                }
                else
                {
                    date = dbAthlete.rslt_date.GetValueOrDefault().ISO8601z();
                }
            }
            else
            {
                date = null;
            }
            place = dbAthlete.rslt_place;
            round = dbAthlete.rslt_round.ToString();
            score = dbAthlete.rslt_score;
            venue = dbAthlete.rslt_venue;
            personalbest = dbAthlete.rslt_personalbest.GetValueOrDefault();
        }

    }

    public class Event
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string brackettype { get; set; }
        public List<Division> division { get; set; }
        public List<Result> results { get; set; }

        public Event(apiGetEventsListByGameID_Result dbSportingEvent)
        {
            guid = dbSportingEvent.evnt_guid;
            name = dbSportingEvent.evnt_name;
        }

        public Event(apiSearchAthleteByEvent_Result2 dbAthlete)
        {
            guid = dbAthlete.evnt_guid;
            name = dbAthlete.evnt_name;
            division = new List<Division>();
            Add(dbAthlete);
        }

        public void Add(apiSearchAthleteByEvent_Result2 dbAthlete)
        {
            if (dbAthlete.EventType == "t")
            {
                if (
                    (division.Count > 0) && (division[division.Count - 1].name == dbAthlete.dvsn_name) &&
                    (division[division.Count - 1].round == dbAthlete.dvsn_round.ToString())
                   )
                {
                    division[division.Count - 1].Add(dbAthlete);
                }
                else
                {
                    division.Add(new Division(dbAthlete));
                }
            }
            else
            {
                if (
                    (division.Count > 0) && (division[division.Count - 1].name == dbAthlete.dvsn_name) &&
                    (division[division.Count - 1].round == dbAthlete.dvsn_round.ToString())
                   )
                {
                    division[division.Count - 1].Add(dbAthlete);
                }
                else
                {
                    division.Add(new Division(dbAthlete));
                }
            }
            
        }

        public Event(apiSearchAthleteByID_Result dbAthlete)
        {
            guid = dbAthlete.evnt_guid;
            name = dbAthlete.evnt_name;
            brackettype = dbAthlete.IsBracketed.ToString();
            results = new List<Result>();
            Add(dbAthlete);
        }

        public void Add(apiSearchAthleteByID_Result dbAthlete)
        {
            results.Add(new Result(dbAthlete));
        }

    }           

    public class Sport
    {
        public string guid { get; set; }
        public string name { get; set; }
        public List<Event> events { get; set; }

        public Sport(apiGetEventsListByGameID_Result dbSportingEvent)
        {
            guid = dbSportingEvent.sprt_Code;
            name = dbSportingEvent.sprt_name;
            events = new List<Event>();
            Add(dbSportingEvent);
        }

        public void Add(apiGetEventsListByGameID_Result dbSportingEvent)
        {
            events.Add(new Event(dbSportingEvent));
        }

        public Sport(apiSearchAthleteByID_Result dbAthlete)
        {
            guid = dbAthlete.sprt_guid;
            name = dbAthlete.sprt_name;
            events = new List<Event>();
            Add(dbAthlete);
        }

        public void Add(apiSearchAthleteByID_Result dbAthlete)
        {
            if ((events.Count > 0) && (events[events.Count - 1].guid == dbAthlete.evnt_guid))
            {
                events[events.Count-1].Add(dbAthlete);
            }
            else
            {
                events.Add(new Event(dbAthlete));
            }
        }

    }
    
    public class Game
    {
        public string guid { get; set; }
        public string name { get; set; }
        public List<Sport> sports { get; set; }

        public Game(apiSearchAthleteByID_Result dbAthlete)
        {
            guid = dbAthlete.game_guid;
            name = dbAthlete.game_name;
            sports = new List<Sport>();
            Add(dbAthlete);
        }

        public void Add(apiSearchAthleteByID_Result dbAthlete)
        {
            if ((sports.Count > 0) && (sports[sports.Count - 1].guid == dbAthlete.sprt_guid))
            {
                sports[sports.Count-1].Add(dbAthlete);
            }
            else
            {
                sports.Add(new Sport(dbAthlete));
            }
        }

    }
   
    public class Athlete
    {
        public string guid { get; set; }
        public Name name { get; set; }
        public string imagepath { get; set; }
        public Delegation delegation { get; set; }
        public string group { get; set; }
        public string bib { get; set; }        
        public List<Game> games { get; set; }
        public string date { get; set; }
        public string score { get; set; }
        public string place { get; set; }
        public string matchid { get; set; }
        public string matchstatus { get; set; }
        public string divisionstatus { get; set; }
        public string venue { get; set; }
        public string personalbest { get; set; }

        public Athlete(apiSearchAthleteByEvent_Result2 dbAthlete, bool byTeam)
        {
            guid = dbAthlete.athl_guid;
            name = new Name(dbAthlete);
            imagepath = dbAthlete.athl_imagepath;
            bib = dbAthlete.athl_bib;

            if (!byTeam)
            {
                delegation = new Delegation(dbAthlete);
                group = dbAthlete.athl_group;

                if (dbAthlete.athl_date.HasValue)
                {
                    DateTime tmpDate = dbAthlete.athl_date.GetValueOrDefault();
                    if (tmpDate.Year == 1899)
                    {
                        date = null;
                    }
                    else
                    {
                        date = dbAthlete.athl_date.GetValueOrDefault().ISO8601z();
                    }
                }
                else
                {
                    date = null;
                }

                score = dbAthlete.athl_score;
                place = dbAthlete.athl_place;
                matchid = string.IsNullOrEmpty(dbAthlete.rslt_matchid) ? "" : dbAthlete.rslt_matchid;
                matchstatus = String.IsNullOrEmpty(dbAthlete.athl_matchstatus) ? "" : dbAthlete.athl_matchstatus;
                divisionstatus = dbAthlete.athl_divisionstatus;
                venue = dbAthlete.athl_venue;
                personalbest = dbAthlete.athl_personalbest.GetValueOrDefault().ToString();
            }
        }

        public Athlete(apiSearchAthleteByID_Result dbAthlete)
        {
            guid = dbAthlete.athl_guid;
            name = new Name(dbAthlete);
            delegation = new Delegation(dbAthlete);
            group = dbAthlete.athl_group;
            imagepath = dbAthlete.athl_imagepath;            
            games = new List<Game>();
            Add(dbAthlete);
        }

        public void Add(apiSearchAthleteByID_Result dbAthlete)
        {
            if ((games.Count > 0) && (games[games.Count - 1].guid == dbAthlete.game_guid))
            {
                games[games.Count-1].Add(dbAthlete);
            }
            else
            {
                games.Add(new Game(dbAthlete));
            }
        }

    }

    public class Team
    {
        public string guid { get; set; }
        public string name { get; set; }
        public Delegation delegation { get; set; }
        public string group { get; set; }
        public string date { get; set; }
        public string score { get; set; }
        public string place { get; set; }
        public string matchid { get; set; }
        public string matchstatus { get; set; }
        public string divisionstatus { get; set; }
        public string venue { get; set; }
        public bool personalbest { get; set; }
        public List<Athlete> athletes { get; set; }

        public Team(apiSearchAthleteByEvent_Result2 dbAthlete)
        {
            guid = dbAthlete.team_guid;
            name = dbAthlete.team_name;
            delegation = new Delegation(dbAthlete);
            group = dbAthlete.athl_group;

            if (dbAthlete.athl_date.HasValue)
            {
                DateTime tmpDate = dbAthlete.athl_date.GetValueOrDefault();
                if (tmpDate.Year == 1899)
                {
                    date = null;
                }
                else
                {
                    date = dbAthlete.athl_date.GetValueOrDefault().ISO8601z();
                }
            }
            else
            {
                date = null;
            }

            score = dbAthlete.athl_score;
            place = dbAthlete.athl_place;
            matchid = string.IsNullOrEmpty(dbAthlete.rslt_matchid) ? "" : dbAthlete.rslt_matchid;
            matchstatus = dbAthlete.athl_matchstatus;
            divisionstatus = dbAthlete.athl_divisionstatus;
            venue = dbAthlete.athl_venue;
            personalbest = dbAthlete.athl_personalbest.GetValueOrDefault();
            athletes = new List<Athlete>();
            Add(dbAthlete);
        }

        public void Add(apiSearchAthleteByEvent_Result2 dbAthlete)
        {
            athletes.Add(new Athlete(dbAthlete, true));
        }

    }

    public class AthletesByIdRootObject
    {
        public List<Athlete> athletes { get; set; }

        public AthletesByIdRootObject()
        {
            athletes = new List<Athlete>();
        }

        public void Add(apiSearchAthleteByID_Result dbAthlete)
        {
            if ((athletes.Count > 0) && (athletes[athletes.Count - 1].guid == dbAthlete.athl_guid))
            {
                athletes[athletes.Count - 1].Add(dbAthlete);
            }
            else
            {
                athletes.Add(new Athlete(dbAthlete));
            }
        }        
    }

    public class AthletesSearchByEventRootObject
    {
        public List<Event> events { get; set; }

        public AthletesSearchByEventRootObject()
        {
            events = new List<Event>();
        }

        public void Add(apiSearchAthleteByEvent_Result2 dbAthlete)
        {
            if ((events.Count > 0) && (events[events.Count - 1].guid == dbAthlete.evnt_guid))
            {
                events[events.Count - 1].Add(dbAthlete);
            }
            else
            {
                events.Add(new Event(dbAthlete));
            }
        }
    }

    public class ScheduledEvent
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string venue { get; set; }
        public int round { get; set; }
       
        public ScheduledEvent(apiGetGamesSchedule_Result dbSchedule)
        {
            guid = dbSchedule.evnt_guid;
            name = dbSchedule.evnt_name;
            round = dbSchedule.evnt_round;
            venue = dbSchedule.evnt_venue;
        }
    }

    public class ScheduledSport
    {
        public string guid { get; set; }
        public string name { get; set; }
        public List<ScheduledEvent> events { get; set; }

        public ScheduledSport(apiGetGamesSchedule_Result dbSchedule)
        {
            guid = dbSchedule.sprt_guid;
            name = dbSchedule.sprt_name;
            events = new List<ScheduledEvent>();
            Add(dbSchedule);
        }

        public void Add(apiGetGamesSchedule_Result dbSchedule)
        {
            events.Add(new ScheduledEvent(dbSchedule));
        }

    }

    public class ScheduledTime
    {
        public string startime { get; set; }
        public List<ScheduledSport> sports { get; set; }

        public ScheduledTime(apiGetGamesSchedule_Result dbSchedule)
        {
            startime = dbSchedule.schd_time.GetValueOrDefault().ISO8601z();
            sports = new List<ScheduledSport>();
            Add(dbSchedule);
        }

        public void Add(apiGetGamesSchedule_Result dbSchedule)
        {
            if ((sports.Count > 0) && (sports[sports.Count - 1].guid == dbSchedule.sprt_guid))
            {
                sports[sports.Count - 1].Add(dbSchedule);
            }
            else
            {
                sports.Add(new ScheduledSport(dbSchedule));
            }
        }

    }

    public class Schedule
    {
        public string date { get; set; }
        public List<ScheduledTime> times { get; set; }

        public Schedule(apiGetGamesSchedule_Result dbSchedule)
        {
            if (dbSchedule.schd_date.HasValue)
            {
                DateTime tmpDate = dbSchedule.schd_date.GetValueOrDefault();
                if (tmpDate.Year == 1899)
                {
                    date = null;
                }
                else
                {
                    date = dbSchedule.schd_date.GetValueOrDefault().ISO8601z();
                }
            }
            else
            {
                date = null;
            }
            
            times = new List<ScheduledTime>();
            Add(dbSchedule);
        }

        public void Add(apiGetGamesSchedule_Result dbSchedule)
        {
            if ((times.Count > 0) &&
                (times[times.Count - 1].startime == dbSchedule.schd_time.GetValueOrDefault().ISO8601z()))
            {
                times[times.Count - 1].Add(dbSchedule);
            }
            else
            {
                times.Add(new ScheduledTime(dbSchedule));
            }
        }

    }

    public class ScheduledEventsRootObject
    {
        public List<Schedule> schedule { get; set; }

        public ScheduledEventsRootObject()
        {
            schedule = new List<Schedule>();
        }

        public void Add(apiGetGamesSchedule_Result dbSchedule)
        {
            if ((schedule.Count > 0) &&
                (schedule[schedule.Count - 1].date == dbSchedule.schd_date.GetValueOrDefault().ISO8601z()))
            {
                schedule[schedule.Count - 1].Add(dbSchedule);
            }
            else
            {
                schedule.Add(new Schedule(dbSchedule));
            }
        }

    }
}