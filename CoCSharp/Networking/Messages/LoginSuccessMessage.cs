﻿using System;

namespace CoCSharp.Networking.Messages
{
    /// <summary>
    /// Message that is sent by the server to the client is
    /// <see cref="LoginRequestMessage"/> was successful.
    /// </summary>
    public class LoginSuccessMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginSuccessMessage"/> class.
        /// </summary>
        public LoginSuccessMessage()
        {
            // Space
        }

        /// <summary>
        /// User ID of the client.
        /// </summary>
        public long UserID;
        /// <summary>
        /// User ID of the client. Same as <see cref="UserID"/>.
        /// </summary>
        public long UserID1;
        /// <summary>
        /// User token of the client.
        /// </summary>
        public string UserToken;
        /// <summary>
        /// Facebook ID.
        /// </summary>
        public string FacebookID;
        /// <summary>
        /// Game center ID.
        /// </summary>
        public string GameCenterID;
        /// <summary>
        /// Major version.
        /// </summary>
        public int MajorVersion;
        /// <summary>
        /// Minor version.
        /// </summary>
        public int MinorVersion;
        /// <summary>
        /// Revision version.
        /// </summary>
        public int RevisionVersion;
        /// <summary>
        /// Environment of the server.
        /// </summary>
        public string ServerEnvironment; // could implment Enum here
        /// <summary>
        /// Number of times logged in.
        /// </summary>
        public int LoginCount;
        /// <summary>
        /// Amount of time logged in.
        /// </summary>
        public TimeSpan PlayTime;

        /// <summary>
        /// Unknown integer 1.
        /// </summary>
        public int Unknown1;

        /// <summary>
        /// Facebook app ID.
        /// </summary>
        public string FacebookAppID;
        /// <summary>
        /// Date last logged in.
        /// </summary>
        public DateTime DateLastPlayed;
        /// <summary>
        /// Date joined the server.
        /// </summary>
        public DateTime DateJoined;

        /// <summary>
        /// Unknown integer 2.
        /// </summary>
        public int Unknown2;

        /// <summary>
        /// Google plus ID.
        /// </summary>
        public string GooglePlusID;
        /// <summary>
        /// Country code.
        /// </summary>
        public string CountryCode;


        /// <summary>
        ///  Gets the ID of the <see cref="LoginSuccessMessage"/>.
        /// </summary>
        public override ushort ID { get { return 20104; } }

        /// <summary>
        /// Reads the <see cref="LoginSuccessMessage"/> from the specified <see cref="MessageReader"/>.
        /// </summary>
        /// <param name="reader">
        /// <see cref="MessageReader"/> that will be used to read the <see cref="LoginSuccessMessage"/>.
        /// </param>
        public override void ReadMessage(MessageReader reader)
        {
            UserID = reader.ReadInt32();
            UserID1 = reader.ReadInt32();
            UserToken = reader.ReadString();
            FacebookID = reader.ReadString();
            GameCenterID = reader.ReadString();
            MajorVersion = reader.ReadInt32();
            MinorVersion = reader.ReadInt32();
            RevisionVersion = reader.ReadInt32();
            ServerEnvironment = reader.ReadString();
            LoginCount = reader.ReadInt32();
            PlayTime = TimeSpan.FromSeconds(reader.ReadInt32());

            Unknown1 = reader.ReadInt32();

            FacebookAppID = reader.ReadString();
            DateLastPlayed = DateTimeConverter.FromJavaTimestamp(double.Parse(reader.ReadString()));
            DateJoined = DateTimeConverter.FromJavaTimestamp(double.Parse(reader.ReadString()));

            Unknown2 = reader.ReadInt32();

            GooglePlusID = reader.ReadString();
            CountryCode = reader.ReadString();
        }

        /// <summary>
        /// Writes the <see cref="LoginSuccessMessage"/> to the specified <see cref="MessageWriter"/>.
        /// </summary>
        /// <param name="writer">
        /// <see cref="MessageWriter"/> that will be used to write the <see cref="LoginSuccessMessage"/>.
        /// </param>
        public override void WriteMessage(MessageWriter writer)
        {
            writer.Write(UserID);
            writer.Write(UserID1);
            writer.Write(UserToken);
            writer.Write(FacebookID);
            writer.Write(GameCenterID);
            writer.Write(MajorVersion);
            writer.Write(MinorVersion);
            writer.Write(RevisionVersion);
            writer.Write(ServerEnvironment);
            writer.Write(LoginCount);
            writer.Write((int)PlayTime.TotalSeconds);

            writer.Write(Unknown1);

            writer.Write(FacebookAppID);
            writer.Write(DateTimeConverter.ToJavaTimestamp(DateLastPlayed).ToString());
            writer.Write(DateTimeConverter.ToJavaTimestamp(DateJoined).ToString());

            writer.Write(Unknown2);

            writer.Write(GooglePlusID);
            writer.Write(CountryCode);
        }
    }
}