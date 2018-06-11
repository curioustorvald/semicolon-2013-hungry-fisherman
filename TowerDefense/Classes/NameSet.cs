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
            public const int TOWER_FISHER_1 = 10001;
            public const int TOWER_FISHER_2 = 10002;
            public const int TOWER_FISHER_3 = 10003;
            public const int TOWER_FISHER_4 = 10004;

            public const int TOWER_HARPOON = 10010; //작살
            public const int TOWER_HARPOON_1 = 10011;
            public const int TOWER_HARPOON_2 = 10012;
            public const int TOWER_HARPOON_3 = 10013;
            public const int TOWER_HARPOON_4 = 10014;

            public const int TOWER_FISHING_NET = 10020; //그물
            public const int TOWER_FISHING_NET_1 = 10021;
            public const int TOWER_FISHING_NET_2 = 10022;
            public const int TOWER_FISHING_NET_3 = 10023;
            public const int TOWER_FISHING_NET_4 = 10024;
            
            public const int TOWER_DISCHARGE = 10030; //전기
            public const int TOWER_DISCHARGE_1 = 10031;
            public const int TOWER_DISCHARGE_2 = 10032;
            public const int TOWER_DISCHARGE_3 = 10033;
            public const int TOWER_DISCHARGE_4 = 10034;
            
            public const int TOWER_BAIT = 10040; //미끼
            public const int TOWER_BAIT_1 = 10041;
            public const int TOWER_BAIT_2 = 10042;
            public const int TOWER_BAIT_3 = 10043;
            public const int TOWER_BAIT_4 = 10044;
            
            public const int TOWER_STONE_THROWER = 10050; //돌 던지기
            public const int TOWER_STONE_THROWER_1 = 10051;
            public const int TOWER_STONE_THROWER_2 = 10052;
            public const int TOWER_STONE_THROWER_3 = 10053;
            public const int TOWER_STONE_THROWER_4 = 10054;

            public const int TOWER_TEST = 10900;
        }
        public class Mob
        {
            public const int DEFAULT_1 = 20000; //멸치
            public const int DEFAULT_2 = 20010; //흰동가리
            public const int DEFAULT_3 = 20020; //복어
            public const int DEFAULT_4 = 20030; //망둥어
            public const int DEFAULT_5 = 20040; //가오리

            public const int FLYING_FISH = 20050; //날치
            public const int SQUID = 20060;
            public const int TRANSPARENT = 20070;
            public const int SKELETON = 20080;
            public const int JELLYFISH = 2090;

            public const int TORTOISE = 20100; //거북
            public const int PYTHON = 20110; //물뱀
            public const int ANGLERFISH = 20120;

            public const int SHARK = 20130;
            public const int WHALE = 20140;

            public const int TESTMOB = 20900;
        }
        public class Entity
        {
            public const int ENTITY_HARPOON_FIRED = 11000; //작살
            public const int ENTITY_BAIT_THROWN = 11010; //미끼
            public const int ENTITY_STONE_THROWN = 11020; //던져진 돌
            public const int ENTITY_NET_THROWN = 11030;
        }
        public class Res
        {
            public const int BUTTON_01_RELEASE = 50000;
            public const int LOADINGBACKGROUND = 90000;
            public const int VAR = 90001;
            public const int SPLASH = 90002;

            public const int START_BUTTON = 50001;
            public const int MAIN_BACK = 50001;
            
            public const int GAME_MENU = 50002;
            public const int GAME_BOARD = 50003;

            public const int STORYLINE_IMAGE = 50004;

            public const int STAGE_SELECT_BACKGROUND = 50004;
            public const int STAGE_SELECT_1 = 50005;
            public const int STAGE_SELECT_2 = 50006;
            public const int STAGE_SELECT_3 = 50006;

            public const int STAGE_ONE = 50010;
            public const int STAGE_ONE_BUILDABLE = 50011;
            public const int STAGE_TWO = 50020;
            public const int STAGE_TWO_BUILDABLE = 50021;
            public const int STAGE_THREE = 50030;
            public const int STAGE_THREE_BUILDABLE = 50031;

			public const int IMAGE_NUMBER_0 = 50040;
			public const int IMAGE_NUMBER_1 = 50041;
			public const int IMAGE_NUMBER_2 = 50042;
			public const int IMAGE_NUMBER_3 = 50043;
			public const int IMAGE_NUMBER_4 = 50044;
			                                      
			public const int IMAGE_NUMBER_5 = 50045;
			public const int IMAGE_NUMBER_6 = 50046;
			public const int IMAGE_NUMBER_7 = 50047;
			public const int IMAGE_NUMBER_8 = 50048;
			public const int IMAGE_NUMBER_9 = 50049;

            public const int SIDEBAR_1 = 50050;
            public const int SIDEBAR_2 = 50051;

            public const int BAIT_UPGRADE = 50052;
            public const int FISHING_UPGRADE = 50053;
            public const int HARPOON_UPGRADE = 50054;
            public const int NET_UPGRADE = 50055;
            public const int STONE_UPGRADE = 50056;

            public const int TOWER_BUTTON_1 = 50057;
            public const int TOWER_BUTTON_2 = 50058;
            public const int TOWER_BUTTON_3 = 50059;
            public const int TOWER_BUTTON_4 = 50060;
            public const int TOWER_BUTTON_5 = 50061;

            public const int HOME_BUTTON = 50062;
            public const int OPTION_BUTTON = 50063;
            public const int SOUND_TOGGLE_BUTTON = 50064;
            public const int GO_BUTTON = 50065;
            public const int STOP_BUTTON = 50066;
            public const int DESELECT_BUTTON = 50067;

            public const int ESCMENU_BACKGROUND = 50070;
            public const int ESCMENU_RESUME = 50071;
            public const int ESCMENU_RESTART = 50072;
            public const int ESCMENU_EXIT = 50073;

            public const int SELL_BUTTON = 50074;

            public const int MAPSEL_1 = 50080;
            public const int MAPSEL_2 = 50081;
            public const int MAPSEL_3 = 50082;
        }
        public class Audio
        {
            public const int THROW_HARPOON = 40000;
            public const int THROW_NET = 40001;
            public const int THROW_STONE = 40002;
        }

    }
}
