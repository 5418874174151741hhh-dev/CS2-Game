using UnityEngine;

/// <summary>
/// 游戏常量定义
/// </summary>
public static class Constants
{
    // 游戏状态
    public static class GameState
    {
        public const string MENU = "Menu";
        public const string LOADING = "Loading";
        public const string PLAYING = "Playing";
        public const string PAUSED = "Paused";
        public const string ROUND_END = "RoundEnd";
        public const string GAME_OVER = "GameOver";
    }

    // 玩家
    public static class Player
    {
        public const float MAX_HEALTH = 100f;
        public const float MAX_ARMOR = 100f;
        public const float PLAYER_SPEED = 5f;
        public const float PLAYER_SPRINT_SPEED = 8f;
        public const float JUMP_FORCE = 5f;
    }

    // 武器
    public static class Weapon
    {
        public const int MAX_WEAPONS = 5;
        public const float DEFAULT_FIRE_RATE = 0.1f;
        public const float DEFAULT_RELOAD_TIME = 2.5f;
    }

    // 伤害
    public static class Damage
    {
        public const float HEAD_MULTIPLIER = 2.5f;
        public const float BODY_MULTIPLIER = 1.0f;
        public const float LEG_MULTIPLIER = 0.75f;
        public const float DISTANCE_FALLOFF = 100f; // 最大有效距离
    }

    // 回合
    public static class Round
    {
        public const float BUY_TIME = 45f; // 购买阶段时间
        public const float BATTLE_TIME = 40f; // 战斗阶段时间
        public const float ROUND_END_TIME = 10f; // 回合结束阶段
        public const int ROUNDS_TO_WIN = 13; // 赢得比赛所需的回合数
    }

    // 经济
    public static class Economy
    {
        public const float STARTING_MONEY = 2400f;
        public const float MAX_MONEY = 16000f;
        public const float KILL_REWARD = 300f;
        public const float ASSIST_REWARD = 100f;
        public const float WIN_REWARD = 3500f;
        public const float LOSS_REWARD = 1900f;
        public const float BOMB_DEFUSE_REWARD = 3500f;
    }

    // 队伍
    public static class Team
    {
        public const int TEAM_CT = 0; // Counter-Terrorist
        public const int TEAM_T = 1;  // Terrorist
        public const int MAX_PLAYERS_PER_TEAM = 5;
    }

    // 层
    public static class Layers
    {
        public const string PLAYER = "Player";
        public const string ENEMY = "Enemy";
        public const string ENVIRONMENT = "Environment";
        public const string PROJECTILE = "Projectile";
    }
}
