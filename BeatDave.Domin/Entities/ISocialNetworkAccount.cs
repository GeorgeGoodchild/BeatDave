﻿
namespace BeatDave.Domain
{
    public interface ISocialNetworkAccount
    {
        string SocialNetworkName { get; }

        bool Share(LogBookEntry entry);
    }
}