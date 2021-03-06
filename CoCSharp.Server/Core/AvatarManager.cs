﻿using CoCSharp.Logic;
using System;
using System.Collections.Generic;
using System.IO;

namespace CoCSharp.Server.Core
{
    // Provides method to save & load avatars.
    // TODO: Implement thread safety.
    public class AvatarManager // : IAvatarManager
    {
        public AvatarManager()
        {
            LoadedAvatar = new Dictionary<string, Avatar>();

            if (!Directory.Exists(DirectoryPaths.Avatars))
                Directory.CreateDirectory(DirectoryPaths.Avatars);
        }

        public Dictionary<string, Avatar> LoadedAvatar { get; private set; }

        //// Cache to the path to avatar directories to reduce the number of calls to Directory.GetDirectories().
        //private Dictionary<string, string> _avatarDirectories;

        private int _maxUserID = 0;

        // Creates a new Avatar with a random Token & UserID.
        public Avatar CreateNewAvatar()
        {
            // Generate a unique token.
            var token = TokenUtils.GenerateToken();
            while (Exists(token))
                token = TokenUtils.GenerateToken();

            // Making searches by UserID easier for the CPU.
            var userID = ++_maxUserID;

            return CreateNewAvatar(token, userID);
        }

        // Creates a new Avatar with the specified Token & UserID.
        public Avatar CreateNewAvatar(string token, long id)
        {
            var villagePath = Path.Combine(DirectoryPaths.Content, "starting_village.json");
            var avatar = new Avatar();
            avatar.ShieldEndTime = DateTime.UtcNow.AddDays(3);
            avatar.Token = token;
            avatar.ID = id;
            avatar.Level = 10; // Bypass tut
            avatar.Home = Village.FromJson(File.ReadAllText(villagePath));
            avatar.Name = "Patrik"; // :]
            avatar.Gems = 300;
            avatar.FreeGems = 300;

            return avatar;
        }

        // Loads the avatar from disk with the specified token.
        public Avatar LoadAvatar(string token)
        {
            if (!Exists(token))
                throw new ArgumentException("Avatar with token '" + token + "' does not exists.", "token");

            FancyConsole.WriteLine("[&(magenta)Avatar&(default)] Loading avatar ->");

            var avatar = new Avatar() { Token = token };
            var avatarSave = new AvatarSave(avatar);
            avatarSave.Load();

            //LoadedAvatars.Add(avatar.Token, avatar);
            return avatar;
        }

        // Saves the avatar to disk.
        public void SaveAvatar(Avatar avatar)
        {
            var avatarSave = new AvatarSave(avatar);
            avatarSave.Save();
        }

        // Determines if an Avatar with the specified token exists.
        public bool Exists(string token)
        {
            var directories = Directory.GetDirectories(DirectoryPaths.Avatars);
            for (int i = 0; i < directories.Length; i++)
            {
                var directory = Path.GetFileName(directories[i]);
                if (directory == token)
                    return true;
            }
            return false;
        }
    }
}
