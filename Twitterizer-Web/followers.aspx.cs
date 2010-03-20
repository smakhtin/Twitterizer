﻿//-----------------------------------------------------------------------
// <copyright file="followers.aspx.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://code.google.com/p/twitterizer/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <summary>The followers example page.</summary>
//-----------------------------------------------------------------------

using System;
using System.Web.UI.WebControls;
using Twitterizer;

public partial class followers : System.Web.UI.Page
{
    public TwitterUserCollection FollowersCollection { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.FollowersCollection = TwitterUser.GetFollowers(Master.Tokens);
            ViewState.Add("followers", this.FollowersCollection);
            this.DataBind();
        }
        else
        {
            this.FollowersCollection = ViewState["followers"] as TwitterUserCollection;
        }
    }

    protected string SafeBooleanText(bool? value)
    {
        if (value == null)
        {
            return "No (Null)";
        }

        return value == true ? "Yes" : "No";
    }

    protected void NextPageLinkButton_Click(object sender, EventArgs e)
    {
        this.FollowersCollection = this.FollowersCollection.NextPage();
        this.DataBind();

        ViewState["followers"] = this.FollowersCollection;
    }

    protected void FollowersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        TwitterUser dataItem = e.Row.DataItem as TwitterUser;

        if (dataItem == null)
        {
            return;
        }

        e.Row.BackColor = dataItem.ProfileBackgroundColor;
        e.Row.ForeColor = dataItem.ProfileTextColor;
    }

    protected void FollowersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DrillDownUser")
        {
            int rowIndex = int.Parse((string)e.CommandArgument);
            long userId = (long)FollowersGridView.DataKeys[rowIndex].Value;

            this.FollowersCollection = TwitterUser.GetFollowers(Master.Tokens, userId);
            ViewState["followers"] = this.FollowersCollection;
            this.DataBind();
        }
    }
}
