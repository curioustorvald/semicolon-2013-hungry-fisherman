using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefense
{
    public class NameSet
    {
        /* 프로그램에 사용되는 모든 이름을 정의함
         * 모든 몹, 타워 등(Classifier)은 아래의 이름을 사용함
         * 새 이름을 정의하려면 아래에 추가 바람
         */
        public class Tower
        {
            /*
            public const int TOWER_CL0 = 10000;
            public const int TOWER_CL1 = 10001;
            public const int TOWER_CL2 = 10002;
            public const int TOWER_CL3 = 10003;
             */

            public const int TOWER_FISHER = 10000; //낚시
            public const int TOWER_HARPOON = 10010; //작살
            public const int TOWER_FISHING_NET = 10020; //그물
            public const int TOWER_DISCHARGE = 10030; //전기
            public const int TOWER_BAIT = 10040; //미끼
            public const int TOWER_STONE_THROWER = 10050; //돌 던지기
            public const int TOWER_FISHER_SUPER = 10060; //강태공


            public const int TOWER_TEST = 10900;
        }
        public class Mob
        {
            public const int DEFAULT_1 = 20000; //잡몹
            public const int DEFAULT_2 = 20010;
            public const int DEFAULT_3 = 20020;
            public const int DEFAULT_4 = 20030;
            public const int DEFAULT_5 = 20040;

            public const int FLYING_FISH = 20050; //날치
            public const int CRAB = 20060;
            public const int SHIT = 20070;
            public const int CLOAKING = 20080; //망둥어

            public const int SKELETON = 20090;
            public const int SHIT_2 = 20100;
            public const int JELLYFISH = 20110;
            public const int TORTOISE = 20120; //거북
            public const int PYTHON = 20130; //물뱀
            public const int CLOAKING_2 = 20140;

            public const int STRIPED_MARLIN = 20150; //청새치
            public const int SHARK = 20160;
            public const int WHALE = 20170;

            public const int TESTMOB = 20900;
        }
        public class Entity
        {
            public const int ENTITY_HARPOON_FIRED = 11000; //작살
            public const int ENTITY_BAIT_THROWN = 11010; //미끼
            public const int ENTITY_STONE_THROWN = 11020; //던져진 돌
        }
        public class AttackType
        {
            /*
            public const int ATT_CL0 = 2000;
            public const int ATT_CL1 = 2001;
            public const int ATT_CL2 = 2002;
            public const int ATT_CL3 = 2003;
             */

            public const int ATTACK_SPLASH_DAMAGE = 2000; //스플래시 데미지
            public const int ATTACK_SHOOTS_PROJECTLE = 2001; //투사체 발사 (작살이나 돌)
            public const int ATTACK_PASSES_THROUGH = 2002; //통과
        }
        public class Res
        {
            public const int BUTTON_01_RELEASE = 50000;
            public const int LOADINGBACKGROUND = 90000;
            public const int VAR = 90001;
            public const int START_BUTTON = 50001;
            public const int MAIN_BACK = 50002;
            public const int GAME_MENU = 50003;
            public const int GAME_BOARD = 50004;
        }

        public class Fonts
        {
            public const int MAIN_FONT = 30000;
        }
    }
}
