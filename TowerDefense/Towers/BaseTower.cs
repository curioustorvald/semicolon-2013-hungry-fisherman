using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TDBase;
using TowerDefense.Player;

namespace TowerDefense.Towers
{
    public abstract class BaseTower
    {
        public string Name; //실제 타워 이름 (게임상에서 보여짐)
        public int Classifier; //코드상에서 사용되는 이름, NameSet.cs 참고

        public ImageSprite spriteSheet; //타워 스프라이트
        //public Button towerButton;

        public int posX;
        public int posY;
        public int heading; //0,1,2,3 = 북, 서, 남, 동

        protected int upgradeTier; //업그레이드 단계 (1-4)
        protected int attackPower; //몹에 주는 데미지
        protected int attackRange; //공격 범위 [픽셀]
        protected int attackCooltime; //공격 쿨타임, [밀리초]
        protected int attackCooltimeCounter; //쿨타임 카운터

        protected int buyPrice; //구매가
        protected int sellPrice; //판매가

        public bool isPresent = true; //현재 타워가 존재하는지 (팔렸는지 안팔렸는지) 여부. 팔렸으면 false

        public int Width = 30; //hitbox
        public int Height = 30; //hitbox

        private int renderW;
        private int renderH;

        protected System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

        public BaseTower(ImageSprite img)
        {
            spriteSheet = img;
            renderW = img.getWidth();
            renderH = img.getHeight();
        }

        public void setStats(string name, int classifier, int power, int range, int speed, int buyPrice, int sellPrice)
        {
            this.Name = name;
            this.Classifier = classifier;
            this.attackPower = power;
            this.attackRange = rangeUnitToPixels(range);
            this.attackCooltime = cooltimeUnitToMiliseconds(speed);
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;

            this.upgradeTier = 0;
        }

        public void Spawn(int x, int y, int r)
        {
            posX = x;
            posY = y;
            heading = r;
        }

        public void BuildTower(int x, int y, int r)
        {
            Spawn(x, y, r);

            Player.Player.addMoney(-buyPrice);
        }

        public abstract void upgrade();

        public void Sell(SceneGame gs)
        {
            isPresent = false;
            Player.Player.addMoney(sellPrice);
            //타워 선택 후 판매를 눌러야 함 ('타워 판매' 모드따윈 음슴)
            if (GameController.selectedTower == -1)
            {
                throw new TDTowerNotSelectedException();
            }
            else
            {
                gs.currentStage.DestroyTower(GameController.selectedTower);
            }
        }

        public int getBuyPrice()
        {
            return buyPrice;
        }

        public int getSellPrice()
        {
            return sellPrice;
        }

		public abstract void Render(SceneGame gs, Graphics g, int fps);

		public abstract void Update(SceneGame gs);

		/*public float distanceFromTower(float x, float y){
			return (float)Math.Pow (Math.Pow ( posX - x, 2 ) + Math.Pow ( posY - y, 2 ), 0.5);
		}

        public float distanceFromTower(Vector v)
        {
            float x = v.getX();
            float y = v.getY();
            return (float)Math.Pow(Math.Pow(posX - x, 2) + Math.Pow(posY - y, 2), 0.5);
        }*/

        public int rangeUnitToPixels(int i)
        {
            return (i + 1) * Width / 2;
        }

        public int cooltimeUnitToMiliseconds(int i)
        {
            return 3000 / i;
        }

        public int getRadius()
        {
            return attackRange;
        }

        public int getTier()
        {
            return upgradeTier;
        }

        public abstract void RenderRange(Graphics g);
    }
}
