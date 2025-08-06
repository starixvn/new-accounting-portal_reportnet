// --------------------------------------------------------------------------
// <copyright file="TeamItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.6.14">Created</change>
// </history>


// --------------------------------------------------------------------------
// <copyright file="TeamItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.6.14">Created</change>
// </history>

using TGR.Model.SoccerPerformanceDetail;

namespace TGR.ViewModel.SoccerPerformanceDetail;

/// <summary>
/// Team view model.
/// </summary>
public class TeamItem
{
    public TeamItem(TeamInfo teamData)
    {
        if (teamData == null)
        {
            return;
        }

        TeamId = teamData.TeamId;
        TeamName = teamData.TeamName;
    }

    public short TeamId { get; set; }

    public string TeamName { get; set; }
}