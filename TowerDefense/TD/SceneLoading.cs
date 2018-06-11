using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Timers;
using System.IO;
using TDBase;
using TowerDefense.Player;

namespace TowerDefense
{
    class SceneLoading : Scene
    {
        //Resources res = Resources.resouces;

        //public const int restotal = 12; // 리소스 개수, 업데이트 바람
        //public int rescurrent = 0;
        //int var = 1;

		private const int splashShowupTime = 3; //
		private float splashTTL = 0; //스플래시 화면이 띄워진 시간 [s]

        //private System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        private int splashFrame = 0;
        private ImageSprite splashImages;

		//Image splashImage = Image.FromFile("images/splash.png");

        Timer times = new Timer() { Interval = 50 };
        delegate void TT();

        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public override void Start()
        {
			//stopWatch.Stop();

            // int, int, int, int, str * 16

            byte[] initialScore = new byte[200];

            for (int i = 0; i < 10; i++){
                initialScore[i * 20] = (byte)(((i + 1) * 1000) >> 24);
                initialScore[i * 20 + 1] = (byte)(((i + 1) * 1000) >> 16);
                initialScore[i * 20 + 2] = (byte)(((i + 1) * 1000) >> 8);
                initialScore[i * 20 + 3] = (byte)((i + 1) * 1000);

                // :-p

                initialScore[i * 20 + 4] = (byte)('M');
                initialScore[i * 20 + 5] = (byte)('A');
                initialScore[i * 20 + 6] = (byte)('D');
                initialScore[i * 20 + 7] = (byte)('-');
                initialScore[i * 20 + 8] = (byte)('D');
                initialScore[i * 20 + 9] = (byte)('R');
                initialScore[i * 20 + 10] = (byte)('A');
                initialScore[i * 20 + 11] = (byte)('G');

                initialScore[i * 20 + 12] = (byte)('O');
                initialScore[i * 20 + 13] = (byte)('N');
                initialScore[i * 20 + 14] = (byte)(0);
                initialScore[i * 20 + 15] = (byte)(0);
                initialScore[i * 20 + 16] = (byte)(0);
                initialScore[i * 20 + 17] = (byte)(0);
                initialScore[i * 20 + 18] = (byte)(0);
                initialScore[i * 20 + 19] = (byte)(0);
            }

                //하이스코어 파일 생성
                if (!new FileInfo(appData + "\\Semicolon\\Hungry Fisherman").Exists)
                {
                    Directory.CreateDirectory(appData + "\\Semicolon\\Hungry Fisherman");
                }

            if (!new FileInfo(appData + "\\Semicolon\\Hungry Fisherman\\highscore_1").Exists)
            {
                using (System.IO.FileStream fs = System.IO.File.Create(appData + "\\Semicolon\\Hungry Fisherman\\highscore_1"))
                {
                    fs.Write(initialScore, 0, initialScore.Length);
                }
            }

			if (!new FileInfo(appData + "\\Semicolon\\Hungry Fisherman\\highscore_2").Exists)
            {
                using (System.IO.FileStream fs = System.IO.File.Create(appData + "\\Semicolon\\Hungry Fisherman\\highscore_2"))
                {
                    fs.Write(initialScore, 0, initialScore.Length);
                }
            }

			if (!new FileInfo(appData + "\\Semicolon\\Hungry Fisherman\\highscore_3").Exists)
            {
                using (System.IO.FileStream fs = System.IO.File.Create(appData + "\\Semicolon\\Hungry Fisherman\\highscore_3"))
                {
                    fs.Write(initialScore, 0, initialScore.Length);
                }
            }

            //Console.WriteLine("[SceneLoading] Created score File");

            //Console.WriteLine("[SceneLoading] Start resource loading");

			/* 리소스 로드 */
        	times.Elapsed += times_Elapsed;
        	
			//최초 로딩
        	//Resources.Add(NameSet.Res.LOADINGBACKGROUND, Image.FromFile("images/Loading.jpg"));
        	
			//로딩바
			//Resources.Add(NameSet.Res.VAR, Image.FromFile("images/var.jpg"));
        	
            //스플래시
            Resources.LAdd(NameSet.Res.SPLASH,
                Image.FromFile("images/splash/1.png"),
                Image.FromFile("images/splash/2.png"),
                Image.FromFile("images/splash/3.png"),
                Image.FromFile("images/splash/4.png"),
                Image.FromFile("images/splash/5.png"),
                Image.FromFile("images/splash/6.png"),
                Image.FromFile("images/splash/7.png"),
                Image.FromFile("images/splash/8.png"),
                Image.FromFile("images/splash/9.png")
                );

            splashImages = new ImageSprite(Resources.LPull(NameSet.Res.SPLASH), 9, 1, 1);
            splashImages.setLocation(320, 240);

			//타이틀 화면
        	Resources.Add(NameSet.Res.MAIN_BACK,
        	    Image.FromFile("images/Main.png")
        	);
        	
			//게임 시작 버튼
			Resources.LAdd(NameSet.Res.START_BUTTON,
        	    Image.FromFile("images/start_0.png"),
        	    Image.FromFile("images/start_1.png"),
        	    Image.FromFile("images/start_2.png")
        	);
        	
            //스토리 이미지
            //Resources.Add(NameSet.Res.STORYLINE_IMAGE, Image.FromFile("storyline.png"));

			//메뉴바
            Resources.LAdd(NameSet.Res.SIDEBAR_1, Image.FromFile("images/sidebar/sidebar_1.png"));
            Resources.LAdd(NameSet.Res.SIDEBAR_2, Image.FromFile("images/sidebar/sidebar_2.png"));

            Resources.LAdd(NameSet.Res.BAIT_UPGRADE,
                Image.FromFile("images/sidebar/bait_upgrade_1.png"),
                Image.FromFile("images/sidebar/bait_upgrade_1_over.png"),
                Image.FromFile("images/sidebar/bait_upgrade_1_click.png"),

                Image.FromFile("images/sidebar/bait_upgrade_2.png"),
                Image.FromFile("images/sidebar/bait_upgrade_2_over.png"),
                Image.FromFile("images/sidebar/bait_upgrade_2_click.png"),

                Image.FromFile("images/sidebar/bait_upgrade_3.png"),
                Image.FromFile("images/sidebar/bait_upgrade_3_over.png"),
                Image.FromFile("images/sidebar/bait_upgrade_3_click.png"),

                Image.FromFile("images/sidebar/bait_upgrade_4.png"),
                Image.FromFile("images/sidebar/bait_upgrade_4_over.png"),
                Image.FromFile("images/sidebar/bait_upgrade_4_click.png")
                );

            Resources.LAdd(NameSet.Res.FISHING_UPGRADE,
                Image.FromFile("images/sidebar/fishing_upgrade_1.png"),
                Image.FromFile("images/sidebar/fishing_upgrade_1_over.png"),
                Image.FromFile("images/sidebar/fishing_upgrade_1_click.png"),
                                               
                Image.FromFile("images/sidebar/fishing_upgrade_2.png"),
                Image.FromFile("images/sidebar/fishing_upgrade_2_over.png"),
                Image.FromFile("images/sidebar/fishing_upgrade_2_click.png"),
                                               
                Image.FromFile("images/sidebar/fishing_upgrade_3.png"),
                Image.FromFile("images/sidebar/fishing_upgrade_3_over.png"),
                Image.FromFile("images/sidebar/fishing_upgrade_3_click.png"),
                                               
                Image.FromFile("images/sidebar/fishing_upgrade_4.png"),
                Image.FromFile("images/sidebar/fishing_upgrade_4_over.png"),
                Image.FromFile("images/sidebar/fishing_upgrade_4_click.png")
                );

            Resources.LAdd(NameSet.Res.HARPOON_UPGRADE,
                Image.FromFile("images/sidebar/harpoon_upgrade_1.png"),
                Image.FromFile("images/sidebar/harpoon_upgrade_1_over.png"),
                Image.FromFile("images/sidebar/harpoon_upgrade_1_click.png"),
                                               
                Image.FromFile("images/sidebar/harpoon_upgrade_2.png"),
                Image.FromFile("images/sidebar/harpoon_upgrade_2_over.png"),
                Image.FromFile("images/sidebar/harpoon_upgrade_2_click.png"),
                                               
                Image.FromFile("images/sidebar/harpoon_upgrade_3.png"),
                Image.FromFile("images/sidebar/harpoon_upgrade_3_over.png"),
                Image.FromFile("images/sidebar/harpoon_upgrade_3_click.png"),
                                               
                Image.FromFile("images/sidebar/harpoon_upgrade_4.png"),
                Image.FromFile("images/sidebar/harpoon_upgrade_4_over.png"),
                Image.FromFile("images/sidebar/harpoon_upgrade_4_click.png")
                );

            Resources.LAdd(NameSet.Res.NET_UPGRADE,
                Image.FromFile("images/sidebar/net_upgrade_1.png"),
                Image.FromFile("images/sidebar/net_upgrade_1_over.png"),
                Image.FromFile("images/sidebar/net_upgrade_1_click.png"),
                                               
                Image.FromFile("images/sidebar/net_upgrade_2.png"),
                Image.FromFile("images/sidebar/net_upgrade_2_over.png"),
                Image.FromFile("images/sidebar/net_upgrade_2_click.png"),
                                               
                Image.FromFile("images/sidebar/net_upgrade_3.png"),
                Image.FromFile("images/sidebar/net_upgrade_3_over.png"),
                Image.FromFile("images/sidebar/net_upgrade_3_click.png"),
                                               
                Image.FromFile("images/sidebar/net_upgrade_4.png"),
                Image.FromFile("images/sidebar/net_upgrade_4_over.png"),
                Image.FromFile("images/sidebar/net_upgrade_4_click.png")
                );

            Resources.LAdd(NameSet.Res.STONE_UPGRADE,
                Image.FromFile("images/sidebar/stone_upgrade_1.png"),
                Image.FromFile("images/sidebar/stone_upgrade_1_over.png"),
                Image.FromFile("images/sidebar/stone_upgrade_1_click.png"),
                                               
                Image.FromFile("images/sidebar/stone_upgrade_2.png"),
                Image.FromFile("images/sidebar/stone_upgrade_2_over.png"),
                Image.FromFile("images/sidebar/stone_upgrade_2_click.png"),
                                               
                Image.FromFile("images/sidebar/stone_upgrade_3.png"),
                Image.FromFile("images/sidebar/stone_upgrade_3_over.png"),
                Image.FromFile("images/sidebar/stone_upgrade_3_click.png"),
                                               
                Image.FromFile("images/sidebar/stone_upgrade_4.png"),
                Image.FromFile("images/sidebar/stone_upgrade_4_over.png"),
                Image.FromFile("images/sidebar/stone_upgrade_4_click.png")
                );

            Resources.LAdd(NameSet.Res.TOWER_BUTTON_1,
                Image.FromFile("images/sidebar/Tower1.png"),
                Image.FromFile("images/sidebar/Tower1_over.png"),
                Image.FromFile("images/sidebar/Tower1_click.png")
                );

            Resources.LAdd(NameSet.Res.TOWER_BUTTON_2,
                Image.FromFile("images/sidebar/Tower2.png"),
                Image.FromFile("images/sidebar/Tower2_over.png"),
                Image.FromFile("images/sidebar/Tower2_click.png")
                );

            Resources.LAdd(NameSet.Res.TOWER_BUTTON_3,
                Image.FromFile("images/sidebar/Tower3.png"),
                Image.FromFile("images/sidebar/Tower3_over.png"),
                Image.FromFile("images/sidebar/Tower3_click.png")
                );

            Resources.LAdd(NameSet.Res.TOWER_BUTTON_4,
                Image.FromFile("images/sidebar/Tower4.png"),
                Image.FromFile("images/sidebar/Tower4_over.png"),
                Image.FromFile("images/sidebar/Tower4_click.png")
                );

            Resources.LAdd(NameSet.Res.TOWER_BUTTON_5,
                Image.FromFile("images/sidebar/Tower5.png"),
                Image.FromFile("images/sidebar/Tower5_over.png"),
                Image.FromFile("images/sidebar/Tower5_click.png")
                );

            Resources.LAdd(NameSet.Res.HOME_BUTTON,
                Image.FromFile("images/sidebar/Home.png"),
                Image.FromFile("images/sidebar/Home_over.png"),
                Image.FromFile("images/sidebar/Home_click.png")
                );

            Resources.LAdd(NameSet.Res.OPTION_BUTTON,
                Image.FromFile("images/sidebar/Option.png"),
                Image.FromFile("images/sidebar/Option_over.png"),
                Image.FromFile("images/sidebar/Option_click.png")
                );

            Resources.LAdd(NameSet.Res.SOUND_TOGGLE_BUTTON,
                Image.FromFile("images/sidebar/Sound.png"),
                Image.FromFile("images/sidebar/Sound_over.png"),
                Image.FromFile("images/sidebar/Sound_click.png")
                );

            Resources.LAdd(NameSet.Res.GO_BUTTON,
                Image.FromFile("images/sidebar/Go.png"),
                Image.FromFile("images/sidebar/Go_over.png"),
                Image.FromFile("images/sidebar/Go_click.png")
                );

            Resources.LAdd(NameSet.Res.STOP_BUTTON,
                Image.FromFile("images/sidebar/Stop.png"),
                Image.FromFile("images/sidebar/Stop_over.png"),
                Image.FromFile("images/sidebar/Stop_click.png")
                );

            Resources.LAdd(NameSet.Res.SELL_BUTTON,
                Image.FromFile("images/sidebar/sell.png"),
                Image.FromFile("images/sidebar/sell_over.png"),
                Image.FromFile("images/sidebar/sell_click.png")
                );

            Resources.LAdd(NameSet.Res.MAPSEL_1,
                Image.FromFile("images/mapselect/map1.png"),
                Image.FromFile("images/mapselect/map1_click.png")
                );

            Resources.LAdd(NameSet.Res.MAPSEL_2,
                Image.FromFile("images/mapselect/map2.png"),
                Image.FromFile("images/mapselect/map2_click.png")
                );

            Resources.LAdd(NameSet.Res.MAPSEL_3,
                Image.FromFile("images/mapselect/map3.png"),
                Image.FromFile("images/mapselect/map3_click.png")
                );

			//스테이지        	
            Resources.Add(NameSet.Res.STAGE_ONE, Image.FromFile("images/map1.png"));
            Resources.Add(NameSet.Res.STAGE_ONE_BUILDABLE, Image.FromFile("images/map1_buildable.png"));

			Resources.Add(NameSet.Res.STAGE_TWO, Image.FromFile("images/map2.png"));
            Resources.Add(NameSet.Res.STAGE_TWO_BUILDABLE, Image.FromFile("images/map2_buildable.png"));

			Resources.Add(NameSet.Res.STAGE_THREE, Image.FromFile("images/map3.png"));
            Resources.Add(NameSet.Res.STAGE_THREE_BUILDABLE, Image.FromFile("images/map3_buildable.png"));

            //엔티티
            Resources.LAdd(NameSet.Entity.ENTITY_HARPOON_FIRED,
                Image.FromFile("images/entities/harpoon_1.png"),
                Image.FromFile("images/entities/harpoon_2.png"),
                Image.FromFile("images/entities/harpoon_3.png")
                );

            Resources.LAdd(NameSet.Entity.ENTITY_STONE_THROWN,
                Image.FromFile("images/entities/stone_1.png"),
                Image.FromFile("images/entities/stone_2.png"),
                Image.FromFile("images/entities/stone_3.png")
                );
            
            Resources.LAdd(NameSet.Entity.ENTITY_NET_THROWN,
                Image.FromFile("images/entities/fishing_net_1.png"),
                Image.FromFile("images/entities/fishing_net_2.png"),
                Image.FromFile("images/entities/fishing_net_larger.png")
                );

            Resources.LAdd(NameSet.Entity.ENTITY_BAIT_THROWN,
                Image.FromFile("images/entities/bait.png")
                );

            //UI 기타
            Resources.LAdd(NameSet.Res.DESELECT_BUTTON, Image.FromFile("images/sidebar/sidebar_deselect.png"));
			//몹
            Resources.LAdd(NameSet.Mob.ANGLERFISH,
                Image.FromFile("images/mobs/anglerfish_1.png"),
                Image.FromFile("images/mobs/anglerfish_2.png"),
                Image.FromFile("images/mobs/anglerfish_3.png"),
                Image.FromFile("images/mobs/anglerfish_4.png")
                );

            Resources.LAdd(NameSet.Mob.DEFAULT_1,
                Image.FromFile("images/mobs/default_1_1.png"),
                Image.FromFile("images/mobs/default_1_2.png"),
                Image.FromFile("images/mobs/default_1_3.png"),
                Image.FromFile("images/mobs/default_1_4.png"),
                Image.FromFile("images/mobs/default_1_5.png"),
                Image.FromFile("images/mobs/default_1_6.png"),
                Image.FromFile("images/mobs/default_1_7.png"),
                Image.FromFile("images/mobs/default_1_8.png")
                );

            Resources.LAdd(NameSet.Mob.DEFAULT_2,
                Image.FromFile("images/mobs/default_2_1.png"),
                Image.FromFile("images/mobs/default_2_2.png"),
                Image.FromFile("images/mobs/default_2_3.png"),
                Image.FromFile("images/mobs/default_2_4.png"),
                Image.FromFile("images/mobs/default_2_5.png"),
                Image.FromFile("images/mobs/default_2_6.png")
                );

            Resources.LAdd(NameSet.Mob.DEFAULT_3,
                Image.FromFile("images/mobs/default_3_1.png"),
                Image.FromFile("images/mobs/default_3_2.png"),
                Image.FromFile("images/mobs/default_3_3.png"),
                Image.FromFile("images/mobs/default_3_4.png"),
                Image.FromFile("images/mobs/default_3_5.png"),
                Image.FromFile("images/mobs/default_3_6.png"),
                Image.FromFile("images/mobs/default_3_7.png"),
                Image.FromFile("images/mobs/default_3_8.png"),
                Image.FromFile("images/mobs/default_3_9.png"),
                Image.FromFile("images/mobs/default_3_10.png"),
                Image.FromFile("images/mobs/default_3_11.png"),
                Image.FromFile("images/mobs/default_3_12.png"),
                Image.FromFile("images/mobs/default_3_13.png"),
                Image.FromFile("images/mobs/default_3_14.png"),
                Image.FromFile("images/mobs/default_3_15.png"),
                Image.FromFile("images/mobs/default_3_16.png"),
                Image.FromFile("images/mobs/default_3_17.png"),
                Image.FromFile("images/mobs/default_3_18.png")
                );

            Resources.LAdd(NameSet.Mob.DEFAULT_4,
                Image.FromFile("images/mobs/default_4_1.png"),
                Image.FromFile("images/mobs/default_4_2.png"),
                Image.FromFile("images/mobs/default_4_3.png"),
                Image.FromFile("images/mobs/default_4_4.png")
                );

            Resources.LAdd(NameSet.Mob.DEFAULT_5,
                Image.FromFile("images/mobs/default_5_1.png"),
                Image.FromFile("images/mobs/default_5_2.png"),
                Image.FromFile("images/mobs/default_5_3.png"),
                Image.FromFile("images/mobs/default_5_4.png"),
                Image.FromFile("images/mobs/default_5_5.png"),
                Image.FromFile("images/mobs/default_5_6.png")
                );

            Resources.LAdd(NameSet.Mob.FLYING_FISH,
                Image.FromFile("images/mobs/flyingfish_1.png"),
                Image.FromFile("images/mobs/flyingfish_2.png"),
                Image.FromFile("images/mobs/flyingfish_3.png"),
                Image.FromFile("images/mobs/flyingfish_4.png"),
                Image.FromFile("images/mobs/flyingfish_5.png"),
                Image.FromFile("images/mobs/flyingfish_6.png")
                );

            Resources.LAdd(NameSet.Mob.JELLYFISH,
                Image.FromFile("images/mobs/jellyfish_1.png"),
                Image.FromFile("images/mobs/jellyfish_2.png"),
                Image.FromFile("images/mobs/jellyfish_3.png")
                );

            Resources.LAdd(NameSet.Mob.PYTHON,
                Image.FromFile("images/mobs/python_1.png"),
                Image.FromFile("images/mobs/python_2.png"),
                Image.FromFile("images/mobs/python_3.png"),
                Image.FromFile("images/mobs/python_4.png")
                );

            Resources.LAdd(NameSet.Mob.SHARK,
                Image.FromFile("images/mobs/shark_1.png"),
                Image.FromFile("images/mobs/shark_2.png"),
                Image.FromFile("images/mobs/shark_3.png"),
                Image.FromFile("images/mobs/shark_4.png")
                );

            Resources.LAdd(NameSet.Mob.SKELETON,
                Image.FromFile("images/mobs/skeleton_1.png"),
                Image.FromFile("images/mobs/skeleton_2.png"),
                Image.FromFile("images/mobs/skeleton_3.png"),
                Image.FromFile("images/mobs/skeleton_4.png")
                );

            Resources.LAdd(NameSet.Mob.SQUID,
                Image.FromFile("images/mobs/squid_1.png"),
                Image.FromFile("images/mobs/squid_2.png"),
                Image.FromFile("images/mobs/squid_3.png"),
                Image.FromFile("images/mobs/squid_4.png")
                );

            Resources.LAdd(NameSet.Mob.TORTOISE,
                Image.FromFile("images/mobs/tortoise_1.png"),
                Image.FromFile("images/mobs/tortoise_2.png"),
                Image.FromFile("images/mobs/tortoise_3.png")
                );

            Resources.LAdd(NameSet.Mob.TRANSPARENT,
                Image.FromFile("images/mobs/transparent_1.png"),
                Image.FromFile("images/mobs/transparent_2.png"),
                Image.FromFile("images/mobs/transparent_3.png"),
                Image.FromFile("images/mobs/transparent_4.png"),
                Image.FromFile("images/mobs/transparent_5.png")
                );

            Resources.LAdd(NameSet.Mob.WHALE,
                Image.FromFile("images/mobs/whale_1.png"),
                Image.FromFile("images/mobs/whale_2.png"),
                Image.FromFile("images/mobs/whale_3.png"),
                Image.FromFile("images/mobs/whale_4.png"),
                Image.FromFile("images/mobs/whale_5.png"),
                Image.FromFile("images/mobs/whale_6.png"),
                Image.FromFile("images/mobs/whale_7.png"),
                Image.FromFile("images/mobs/whale_8.png")
                );
        	
			//타워

            //Stone
            Resources.LAdd(NameSet.Tower.TOWER_STONE_THROWER,
                Image.FromFile("images/towers/stone/0/0.png"),
                Image.FromFile("images/towers/stone/0/1.png"),
                Image.FromFile("images/towers/stone/0/2.png"),
                Image.FromFile("images/towers/stone/0/3.png"),
                Image.FromFile("images/towers/stone/0/4.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_STONE_THROWER_1,
                Image.FromFile("images/towers/stone/1/0.png"),
                Image.FromFile("images/towers/stone/1/1.png"),
                Image.FromFile("images/towers/stone/1/2.png"),
                Image.FromFile("images/towers/stone/1/3.png"),
                Image.FromFile("images/towers/stone/1/4.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_STONE_THROWER_2,
                Image.FromFile("images/towers/stone/2/0.png"),
                Image.FromFile("images/towers/stone/2/1.png"),
                Image.FromFile("images/towers/stone/2/2.png"),
                Image.FromFile("images/towers/stone/2/3.png"),
                Image.FromFile("images/towers/stone/2/4.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_STONE_THROWER_3,
                Image.FromFile("images/towers/stone/3/0.png"),
                Image.FromFile("images/towers/stone/3/1.png"),
                Image.FromFile("images/towers/stone/3/2.png"),
                Image.FromFile("images/towers/stone/3/3.png"),
                Image.FromFile("images/towers/stone/3/4.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_STONE_THROWER_4,
                Image.FromFile("images/towers/stone/4/0.png"),
                Image.FromFile("images/towers/stone/4/1.png"),
                Image.FromFile("images/towers/stone/4/2.png"),
                Image.FromFile("images/towers/stone/4/3.png"),
                Image.FromFile("images/towers/stone/4/4.png")
                );

            //Harpoon
            Resources.LAdd(NameSet.Tower.TOWER_HARPOON,
                Image.FromFile("images/towers/harpoon/0/0.png"),
                Image.FromFile("images/towers/harpoon/0/1.png"),
                Image.FromFile("images/towers/harpoon/0/2.png"),
                Image.FromFile("images/towers/harpoon/0/3.png"),
                Image.FromFile("images/towers/harpoon/0/4.png"),
                Image.FromFile("images/towers/harpoon/0/5.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_HARPOON_1,
                Image.FromFile("images/towers/harpoon/1/0.png"),
                Image.FromFile("images/towers/harpoon/1/1.png"),
                Image.FromFile("images/towers/harpoon/1/2.png"),
                Image.FromFile("images/towers/harpoon/1/3.png"),
                Image.FromFile("images/towers/harpoon/1/4.png"),
                Image.FromFile("images/towers/harpoon/1/5.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_HARPOON_2,
                Image.FromFile("images/towers/harpoon/2/0.png"),
                Image.FromFile("images/towers/harpoon/2/1.png"),
                Image.FromFile("images/towers/harpoon/2/2.png"),
                Image.FromFile("images/towers/harpoon/2/3.png"),
                Image.FromFile("images/towers/harpoon/2/4.png"),
                Image.FromFile("images/towers/harpoon/2/5.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_HARPOON_3,
                Image.FromFile("images/towers/harpoon/3/0.png"),
                Image.FromFile("images/towers/harpoon/3/1.png"),
                Image.FromFile("images/towers/harpoon/3/2.png"),
                Image.FromFile("images/towers/harpoon/3/3.png"),
                Image.FromFile("images/towers/harpoon/3/4.png"),
                Image.FromFile("images/towers/harpoon/3/5.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_HARPOON_4,
                Image.FromFile("images/towers/harpoon/4/0.png"),
                Image.FromFile("images/towers/harpoon/4/1.png"),
                Image.FromFile("images/towers/harpoon/4/2.png"),
                Image.FromFile("images/towers/harpoon/4/3.png"),
                Image.FromFile("images/towers/harpoon/4/4.png"),
                Image.FromFile("images/towers/harpoon/4/5.png")
                );

            //Fisher
            Resources.LAdd(NameSet.Tower.TOWER_FISHER,
                Image.FromFile("images/towers/fishing/0/0.png"),
                Image.FromFile("images/towers/fishing/0/1.png"),
                Image.FromFile("images/towers/fishing/0/2.png"),
                Image.FromFile("images/towers/fishing/0/3.png"),
                Image.FromFile("images/towers/fishing/0/4.png")
                );
            Resources.LAdd(NameSet.Tower.TOWER_FISHER_1,
                Image.FromFile("images/towers/fishing/1/0.png"),
                Image.FromFile("images/towers/fishing/1/1.png"),
                Image.FromFile("images/towers/fishing/1/2.png"),
                Image.FromFile("images/towers/fishing/1/3.png"),
                Image.FromFile("images/towers/fishing/1/4.png")
                );
            Resources.LAdd(NameSet.Tower.TOWER_FISHER_2,
                Image.FromFile("images/towers/fishing/2/0.png"),
                Image.FromFile("images/towers/fishing/2/1.png"),
                Image.FromFile("images/towers/fishing/2/2.png"),
                Image.FromFile("images/towers/fishing/2/3.png"),
                Image.FromFile("images/towers/fishing/2/4.png")
                );
            Resources.LAdd(NameSet.Tower.TOWER_FISHER_3,
                Image.FromFile("images/towers/fishing/3/0.png"),
                Image.FromFile("images/towers/fishing/3/1.png"),
                Image.FromFile("images/towers/fishing/3/2.png"),
                Image.FromFile("images/towers/fishing/3/3.png"),
                Image.FromFile("images/towers/fishing/3/4.png"),
                Image.FromFile("images/towers/fishing/3/5.png")
                );
            Resources.LAdd(NameSet.Tower.TOWER_FISHER_4,
               Image.FromFile("images/towers/fishing/4/0.png"),
               Image.FromFile("images/towers/fishing/4/1.png"),
               Image.FromFile("images/towers/fishing/4/2.png"),
               Image.FromFile("images/towers/fishing/4/3.png"),
               Image.FromFile("images/towers/fishing/4/4.png"),
               Image.FromFile("images/towers/fishing/4/5.png")
               );
            //Bait
            Resources.LAdd(NameSet.Tower.TOWER_BAIT,
               Image.FromFile("images/towers/bait/0/0.png"),
               Image.FromFile("images/towers/bait/0/1.png"),
               Image.FromFile("images/towers/bait/0/2.png")
               );

            Resources.LAdd(NameSet.Tower.TOWER_BAIT_1,
               Image.FromFile("images/towers/bait/1/0.png"),
               Image.FromFile("images/towers/bait/1/1.png"),
               Image.FromFile("images/towers/bait/1/2.png")
               );

            Resources.LAdd(NameSet.Tower.TOWER_BAIT_2,
               Image.FromFile("images/towers/bait/2/0.png"),
               Image.FromFile("images/towers/bait/2/1.png"),
               Image.FromFile("images/towers/bait/2/2.png"),
               Image.FromFile("images/towers/bait/2/3.png")
               );

            Resources.LAdd(NameSet.Tower.TOWER_BAIT_3,
                Image.FromFile("images/towers/bait/3/0.png"),
                Image.FromFile("images/towers/bait/3/1.png"),
                Image.FromFile("images/towers/bait/3/2.png"),
                Image.FromFile("images/towers/bait/3/3.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_BAIT_4,
                Image.FromFile("images/towers/bait/4/0.png"),
                Image.FromFile("images/towers/bait/4/1.png"),
                Image.FromFile("images/towers/bait/4/2.png"),
                Image.FromFile("images/towers/bait/4/3.png")
                );
            //fishingNet
            Resources.LAdd(NameSet.Tower.TOWER_FISHING_NET,
                Image.FromFile("images/towers/fishingNet/0/0.png"),
                Image.FromFile("images/towers/fishingNet/0/1.png"),
                Image.FromFile("images/towers/fishingNet/0/2.png"),
                Image.FromFile("images/towers/fishingNet/0/3.png"),
                Image.FromFile("images/towers/fishingNet/0/4.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_FISHING_NET_1,
                Image.FromFile("images/towers/fishingNet/1/0.png"),
                Image.FromFile("images/towers/fishingNet/1/1.png"),
                Image.FromFile("images/towers/fishingNet/1/2.png"),
                Image.FromFile("images/towers/fishingNet/1/3.png"),
                Image.FromFile("images/towers/fishingNet/1/4.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_FISHING_NET_2,
                Image.FromFile("images/towers/fishingNet/2/0.png"),
                Image.FromFile("images/towers/fishingNet/2/1.png"),
                Image.FromFile("images/towers/fishingNet/2/2.png"),
                Image.FromFile("images/towers/fishingNet/2/3.png"),
                Image.FromFile("images/towers/fishingNet/2/4.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_FISHING_NET_3,
                Image.FromFile("images/towers/fishingNet/3/0.png"),
                Image.FromFile("images/towers/fishingNet/3/1.png"),
                Image.FromFile("images/towers/fishingNet/3/2.png"),
                Image.FromFile("images/towers/fishingNet/3/3.png"),
                Image.FromFile("images/towers/fishingNet/3/4.png")
                );

            Resources.LAdd(NameSet.Tower.TOWER_FISHING_NET_4,
                Image.FromFile("images/towers/fishingNet/4/0.png"),
                Image.FromFile("images/towers/fishingNet/4/1.png"),
                Image.FromFile("images/towers/fishingNet/4/2.png"),
                Image.FromFile("images/towers/fishingNet/4/3.png"),
                Image.FromFile("images/towers/fishingNet/4/4.png")
                );

        	
			//사운드
            Resources.Add(NameSet.Audio.THROW_HARPOON,
                new Audio()
                );

            new MenubarController();
            

            //Console.WriteLine("[SceneLoading] Resource loading done.");

        	times.Start();
        }

        void times_Elapsed(object sender, ElapsedEventArgs e)
        {
            
        }
        public void click()
        {
            //SceneManager.SetScene(new SceneExample());
        }
        public override void Draw(System.Drawing.Graphics graphics, float deltaSeconds)
        {
            SceneManager.SetScene(new SceneSplash());
        }

        public override void MouseDown(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            MouseClass.setMouse(x, y, button, true);
        }

        public override void MouseMove(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            MouseClass.setMouse(x, y, button);
        }

        public override void MouseUp(float x, float y, System.Windows.Forms.MouseButtons button)
        {
            MouseClass.setMouse(x, y, button, false);
        }

        public override void End()
        {
        }

        public override void KeyDown(System.Windows.Forms.Keys key, bool ctrl, bool alt, bool shift)
        {
        }

        public override void KeyUp(System.Windows.Forms.Keys key, bool ctrl, bool alt, bool shift)
        {
        }
    }
}
