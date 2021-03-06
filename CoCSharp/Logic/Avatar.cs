﻿using CoCSharp.Data.Slots;
using CoCSharp.Network.Messages;
using System;

namespace CoCSharp.Logic
{
    /// <summary>
    /// Represents a Clash of Clans avatar.
    /// </summary>
    public class Avatar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Avatar"/> class.
        /// </summary>
        public Avatar()
        {
            // If _level is less that 1 then the client crashes.
            _level = 1;
        }

        /// <summary>
        /// Gets or sets the username of the <see cref="Avatar"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets whether the <see cref="Avatar"/> has been named.
        /// </summary>
        public bool IsNamed { get; set; }

        /// <summary>
        /// Gets or sets the user token of the <see cref="Avatar"/>.
        /// </summary>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not a valid token.</exception>
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                if (!TokenUtils.CheckToken(value))
                    throw new ArgumentException("'" + value + "' is not a valid token.");
                _token = value;
            }
        }
        private string _token;

        /// <summary>
        /// Gets or sets the user ID of the <see cref="Avatar"/>.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Gets the shield duration of the <see cref="Avatar"/>.
        /// </summary>
        public TimeSpan ShieldDuration
        {
            get
            {
                var duration = DateTimeConverter.ToUnixTimestamp(ShieldEndTime) - DateTimeConverter.UnixUtcNow;

                if (duration < 0)
                    return TimeSpan.FromSeconds(0);

                return TimeSpan.FromSeconds(duration);
            }
        }

        /// <summary>
        /// Gets or sets the shield UTC end time of the <see cref="Avatar"/>.
        /// </summary>
        public DateTime ShieldEndTime { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Village"/> associated with this
        /// <see cref="Avatar"/>.
        /// </summary>
        public Village Home { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Clan"/> associated with this
        /// <see cref="Avatar"/>.
        /// </summary>
        public Clan Alliance { get; set; }

        /// <summary>
        /// Gets or sets the league of the <see cref="Avatar"/>.
        /// </summary>
        public int League { get; set; }

        /// <summary>
        /// Gets or sets the level of the <see cref="Avatar"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than 1.</exception>
        public int Level
        {
            get { return _level; }
            set
            {
                // Clash of Clans crashes when level is less than 1.
                if (value < 1)
                    throw new ArgumentOutOfRangeException("value", "value cannot be less than 1.");

                _level = value;
            }
        }
        private int _level;

        /// <summary>
        /// Gets or sets the experience of the <see cref="Avatar"/>.
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Gets or sets the amount of gems of the <see cref="Avatar"/>.
        /// </summary>
        public int Gems { get; set; }

        /// <summary>
        /// Gets or sets the amount of free gems of the <see cref="Avatar"/>.
        /// </summary>
        public int FreeGems { get; set; }

        /// <summary>
        /// Gets or sets the amount of trophies of the <see cref="Avatar"/>.
        /// </summary>
        public int Trophies { get; set; }

        /// <summary>
        /// Gets or sets the number of attacks won by the <see cref="Avatar"/>.
        /// </summary>
        public int AttacksWon { get; set; }

        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Avatar"/>.
        /// </summary>
        public int AttacksLost { get; set; }

        /// <summary>
        /// Gets or sets the number of defenses won by the <see cref="Avatar"/>.
        /// </summary>
        public int DefensesWon { get; set; }

        /// <summary>
        /// Gets or sets the number of defenses lost by the <see cref="Avatar"/>.
        /// </summary>
        public int DefensesLost { get; set; }

        /// <summary>
        /// Gets or sets the resources capacity.
        /// </summary>
        public ResourceCapacitySlot[] ResourcesCapacity { get; set; }

        /// <summary>
        /// Gets or sets the amount of resources available.
        /// </summary>
        public ResourceAmountSlot[] ResourcesAmount { get; set; }

        /// <summary>
        /// Gets or sets the units available.
        /// </summary>
        public UnitSlot[] Units { get; set; }

        /// <summary>
        /// Gets or sets the spells available.
        /// </summary>
        public SpellSlot[] Spells { get; set; }

        /// <summary>
        /// Gets or sets the units upgrades.
        /// </summary>
        public UnitUpgradeSlot[] UnitUpgrades { get; set; }

        /// <summary>
        /// Gets or sets the spells upgrades.
        /// </summary>
        public SpellUpgradeSlot[] SpellUpgrades { get; set; }

        /// <summary>
        /// Gets or sets the heroes upgrades.
        /// </summary>
        public HeroUpgradeSlot[] HeroUpgrades { get; set; }

        /// <summary>
        /// Gets or sets the heroes health.
        /// </summary>
        public HeroHealthSlot[] HeroHealths { get; set; }

        /// <summary>
        /// Gets or sets the heroes states.
        /// </summary>
        public HeroStateSlot[] HeroStates { get; set; }

        /// <summary>
        /// Gets or sets the alliance units.
        /// </summary>
        public AllianceUnitSlot[] AllianceUnits { get; set; }

        /// <summary>
        /// Get or sets the tutorial progress.
        /// </summary>
        public TutorialProgressSlot[] TutorialProgess { get; set; }

        /// <summary>
        /// Gets or sets the achievements state.
        /// </summary>
        public AchievementSlot[] Acheivements { get; set; }

        /// <summary>
        /// Gets or sets the achievements progress.
        /// </summary>
        public AchievementProgessSlot[] AcheivementProgress { get; set; }

        /// <summary>
        /// Gets or sets the NPC stars.
        /// </summary>
        public NpcStarSlot[] NpcStars { get; set; }

        /// <summary>
        /// Gets or sets the NPC gold.
        /// </summary>
        public NpcGoldSlot[] NpcGold { get; set; }

        /// <summary>
        /// Gets or sets the NPC elixir.
        /// </summary>
        public NpcElixirSlot[] NpcElixir { get; set; }

        /// <summary>
        /// Gets a new <see cref="Network.Messages.OwnHomeDataMessage"/> for the
        /// <see cref="Avatar"/>.
        /// </summary>
        public OwnHomeDataMessage OwnHomeDataMessage
        {
            get
            {
                var villageData = new VillageMessageComponent(this);
                var avatarData = new AvatarMessageComponent(this);
                var ohdMessage = new OwnHomeDataMessage()
                {
                    OwnVillageData = villageData,
                    OwnAvatarData = avatarData,
                    Unknown4 = 1462629754000,
                    Unknown5 = 1462629754000,
                    Unknown6 = 1462631554000,
                };

                return ohdMessage;
            }
        }
    }
}
