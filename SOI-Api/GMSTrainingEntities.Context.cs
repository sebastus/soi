﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOI_Api
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GMSTRAININGEntities : DbContext
    {
        public GMSTRAININGEntities()
            : base("name=GMSTRAININGEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<apiGetDelegationsListByGameID_Result> apiGetDelegationsListByGameID(string gameId)
        {
            var gameIdParameter = gameId != null ?
                new ObjectParameter("GameId", gameId) :
                new ObjectParameter("GameId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<apiGetDelegationsListByGameID_Result>("apiGetDelegationsListByGameID", gameIdParameter);
        }
    
        public virtual ObjectResult<apiGetEventsListByGameID_Result> apiGetEventsListByGameID(string gameId)
        {
            var gameIdParameter = gameId != null ?
                new ObjectParameter("GameId", gameId) :
                new ObjectParameter("GameId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<apiGetEventsListByGameID_Result>("apiGetEventsListByGameID", gameIdParameter);
        }
    
        public virtual ObjectResult<apiGetGamesSchedule_Result> apiGetGamesSchedule(string gameId, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, string delegation)
        {
            var gameIdParameter = gameId != null ?
                new ObjectParameter("GameId", gameId) :
                new ObjectParameter("GameId", typeof(string));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            var delegationParameter = delegation != null ?
                new ObjectParameter("Delegation", delegation) :
                new ObjectParameter("Delegation", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<apiGetGamesSchedule_Result>("apiGetGamesSchedule", gameIdParameter, fromDateParameter, toDateParameter, delegationParameter);
        }
    
        public virtual ObjectResult<apiSearchAthleteByID_Result> apiSearchAthleteByID(string bibNumber, string name, string gameId, string delegation, string group)
        {
            var bibNumberParameter = bibNumber != null ?
                new ObjectParameter("BibNumber", bibNumber) :
                new ObjectParameter("BibNumber", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var gameIdParameter = gameId != null ?
                new ObjectParameter("GameId", gameId) :
                new ObjectParameter("GameId", typeof(string));
    
            var delegationParameter = delegation != null ?
                new ObjectParameter("Delegation", delegation) :
                new ObjectParameter("Delegation", typeof(string));
    
            var groupParameter = group != null ?
                new ObjectParameter("Group", group) :
                new ObjectParameter("Group", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<apiSearchAthleteByID_Result>("apiSearchAthleteByID", bibNumberParameter, nameParameter, gameIdParameter, delegationParameter, groupParameter);
        }
    
        public virtual ObjectResult<apiSearchAthleteByEvent_Result2> apiSearchAthleteByEvent(string @event, string gameId)
        {
            var eventParameter = @event != null ?
                new ObjectParameter("Event", @event) :
                new ObjectParameter("Event", typeof(string));
    
            var gameIdParameter = gameId != null ?
                new ObjectParameter("GameId", gameId) :
                new ObjectParameter("GameId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<apiSearchAthleteByEvent_Result2>("apiSearchAthleteByEvent", eventParameter, gameIdParameter);
        }
    }
}
