﻿//-----------------------------------------------------------------------
// <copyright file="TwitterList.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net/)
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
// <summary>The twitter list entity class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using Twitterizer.Core;

    /// <summary>
    /// The twitter list entity class
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [DebuggerDisplay("TwitterList = {FullName}")]
    [Serializable]
    public class TwitterList : TwitterObject
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterList"/> class.
        /// </summary>
        /// <param name="tokens">OAuth access tokens.</param>
        public TwitterList(OAuthTokens tokens) 
            : base()
        {
            this.Tokens = tokens;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterList"/> class.
        /// </summary>
        internal TwitterList()
            : base()
        {
        }
        #endregion

        #region API properties
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The list id.</value>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The list name.</value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>The list slug.</value>
        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the number of subscribers.
        /// </summary>
        /// <value>The number of subscribers.</value>
        [JsonProperty(PropertyName = "subscriber_count")]
        public int NumberOfSubscribers { get; set; }

        /// <summary>
        /// Gets or sets the number of members.
        /// </summary>
        /// <value>The number of members.</value>
        [JsonProperty(PropertyName = "member_count")]
        public int NumberOfMembers { get; set; }

        /// <summary>
        /// Gets or sets the absolute path.
        /// </summary>
        /// <value>The absolute path.</value>
        [JsonProperty(PropertyName = "uri")]
        public string AbsolutePath { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The list mode.</value>
        [JsonProperty(PropertyName = "mode")]
        public string Mode { get; set; }

        /// <summary>
        /// Gets or sets the user that owns the list.
        /// </summary>
        /// <value>The owning user.</value>
        [JsonProperty(PropertyName = "user")]
        public TwitterUser User { get; set; }
        #endregion

        #region Calculated Properties
        /// <summary>
        /// Gets a value indicating whether this instance is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        public bool IsPublic
        {
            get
            {
                return this.Mode == "public";
            }
        }
        #endregion

        #region Static Members
        /// <summary>
        /// Creates a new list.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="name">The list name.</param>
        /// <param name="isPublic">if set to <c>true</c> creates a public list.</param>
        /// <param name="description">The description.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        public static TwitterList New(OAuthTokens tokens, string username, string name, bool isPublic, string description, OptionalProperties options)
        {
            Commands.CreateListCommand command = new Twitterizer.Commands.CreateListCommand(tokens, name, username, options)
            {
                IsPublic = isPublic,
                Description = description
            };

            return Core.CommandPerformer<TwitterList>.PerformAction(command);
        }

        /// <summary>
        /// Modifies a list.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        public static TwitterList Update(OAuthTokens tokens, string username, long listId, UpdateListOptions options)
        {
            Commands.UpdateListCommand command = new Twitterizer.Commands.UpdateListCommand(tokens, username, listId, options);

            return Core.CommandPerformer<TwitterList>.PerformAction(command);
        }

        /// <summary>
        /// List the lists of the specified user. Private lists will be included if the authenticated users is the same as the user who's lists are being returned.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static TwitterListCollection GetLists(OAuthTokens tokens, string username, OptionalProperties options)
        {
            Commands.GetListsCommand command = new Twitterizer.Commands.GetListsCommand(tokens, username, options);
            TwitterListWrapper resultWrapper = Core.CommandPerformer<TwitterListWrapper>.PerformAction(command);

            TwitterListCollection results = resultWrapper.Lists;
            results.Command = command;
            return results;
        }

        /// <summary>
        /// List the lists of the specified user. Private lists will be included if the authenticated users is the same as the user who's lists are being returned.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static TwitterListCollection GetLists(OAuthTokens tokens, string username)
        {
            return GetLists(tokens, username, null);
        }

        /// <summary>
        /// List the lists of the specified user. Private lists will be included if the authenticated users is the same as the user who's lists are being returned.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static TwitterListCollection GetLists(string username, OptionalProperties options)
        {
            return GetLists(null, username, options);
        }

        /// <summary>
        /// List the lists of the specified user. Private lists will be included if the authenticated users is the same as the user who's lists are being returned.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static TwitterListCollection GetLists(string username)
        {
            return GetLists(null, username, null);
        }

        /// <summary>
        /// Gets a single list by id number.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listIdOrSlug">The list id or slug.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static TwitterListCollection GetList(OAuthTokens tokens, string username, string listIdOrSlug, OptionalProperties options)
        {
            Commands.GetListCommand command = new Twitterizer.Commands.GetListCommand(tokens, username, listIdOrSlug, options);

            return Core.CommandPerformer<TwitterListCollection>.PerformAction(command);
        }

        /// <summary>
        /// Deletes the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listIdOrSlug">The list id or slug.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterList"/> instance.</returns>
        public static TwitterList Delete(OAuthTokens tokens, string username, string listIdOrSlug, OptionalProperties options)
        {
            Commands.DeleteListCommand command = new Twitterizer.Commands.DeleteListCommand(tokens, username, listIdOrSlug, options);

            return Core.CommandPerformer<TwitterList>.PerformAction(command);
        }

        /// <summary>
        /// Show tweet timeline for members of the specified list.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection GetStatuses(OAuthTokens tokens, string username, long listId, ListStatusesOptions options)
        {
            Commands.ListStatusesCommand command = new Twitterizer.Commands.ListStatusesCommand(tokens, username, listId, options);

            return Core.CommandPerformer<TwitterStatusCollection>.PerformAction(command);
        }

        /// <summary>
        /// List the lists the specified user has been added to.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static TwitterListCollection GetMemberships(OAuthTokens tokens, string username, OptionalProperties options)
        {
            Commands.ListMembershipsCommand command = new Twitterizer.Commands.ListMembershipsCommand(tokens, username, options);

            return Core.CommandPerformer<TwitterListWrapper>.PerformAction(command).Lists;
        }

        /// <summary>
        /// List the lists the specified user has been added to.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static TwitterListCollection GetMemberships(OAuthTokens tokens, string username)
        {
            return GetMemberships(tokens, username, null);
        }

        /// <summary>
        /// List the lists the specified user follows.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static TwitterListCollection GetSubscriptions(OAuthTokens tokens, OptionalProperties options)
        {
            Commands.GetListSubscriptionsCommand command = new Twitterizer.Commands.GetListSubscriptionsCommand(tokens, options);

            return Core.CommandPerformer<TwitterListWrapper>.PerformAction(command).Lists;
        }

        /// <summary>
        /// List the lists the specified user follows.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterListCollection"/> instance.
        /// </returns>
        public static TwitterListCollection GetSubscriptions(OAuthTokens tokens)
        {
            return GetSubscriptions(tokens, null);
        }
        #endregion
    }
}
