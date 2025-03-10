// --------------------------------------------------------------------------
// <copyright file="BetTypeItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.6.9">Created</change>
// </history>

namespace SoccerPerformanceDetail.ViewModel;

using Model;

/// <summary>
/// Bet type view model.
/// </summary>
[Serializable]
public class BetTypeItem
{
    public BetTypeItem(BetTypeInfo betType)
    {
        if (betType != null)
        {
            BetTypeId = betType.BetType;
            BetTypeName = betType.TypeName;
            IsDefaultCheck = betType.IsDefault;
        }
    }

    public short BetTypeId { get; set; }

    public string BetTypeName { get; set; }

    public bool IsDefaultCheck { get; set; }
}