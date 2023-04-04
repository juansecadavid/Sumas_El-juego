namespace Sumas__TheGame
{
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void PlayerStartsOnAllyTower()
        {
            Tower tower1 = GameManager.TowerCharacterGenerator();

            Assert.IsTrue(tower1.Type.ToString() == Character.type.main.ToString()); //Starts on own tower
            Assert.Less(tower1.FloorList.Count, 3); //few floors on character's tower
        }

        [Test]
        public void PlayerStartsOnRandomFloor()
        {
            int listCount0 = 0;
            int listCount1 = 0;

            for (int i = 0; i < 10000; i++)
            {
                Tower tower1 = GameManager.TowerCharacterGenerator();

                if (tower1.FloorList[0].CharactersList.Count > 0)
                {
                    listCount0++;
                }
                else if (tower1.FloorList[1].CharactersList.Count > 0)
                {
                    listCount1++;
                }

                tower1 = null;
            }

            Assert.IsTrue(listCount0 < 5500 && listCount0 > 4500);
            Assert.IsTrue(listCount1 < 5500 && listCount1 > 4500);

        }

        [Test]
        public void Test3()
        {
            Tower tower1 = GameManager.TowerCharacterGenerator();

            Assert.IsTrue(tower1.Type.ToString() == Character.type.main.ToString()); //Starts on own tower
            Assert.Less(tower1.FloorList.Count, 3); //few floors on character's tower
        }
    }
}