/*
 *      Main.cs
 *      
 *      Copyright 2013 금산고등학교 semicolon 동아리. 모든 권리 보유.
 *      Copyright 2013 'semicolon' of Kumsan Highschool. All rights reserved.
 *      
 *      박혜준 (parkhj1114@gmail.com)
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDBase;

namespace TowerDefense
{
    class Main
    {
        public void Init(SceneManager sceneManager)
        {
            sceneManager.SetScene(new SceneLoading());
        }
    }
}
