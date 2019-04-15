﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Binder.Environment
{

    public class AI : MovableCharacter, ISerialization<AI> 
    {
        public bool isAttacking = false;
        int horizCount = 0;
        int vertCount = 0;
        string horizDirection = "west";
        string vertDirection = "north";
        bool patrolVertically = false;
        int attackTime = 10;
        public AI(int health, int damage, int speed)
        {
            Health = health;
            Damage = damage;
            Speed = speed;
        }

        public void ChangeXFrames(int changeInX)
        {
            if (changeInX > 0)
            {
                if (isAttacking)
                {
                    PictureName = "/Sprites/PsiZetaFrontWhip.png";
                    isAttacking = false;
                }
                else if (attackTime > 5)
                {
                    if (front == 0)
                    {
                        PictureName = "/Sprites/PsiZetaFront.png";
                        front = 1;
                    }
                    else if (front == 1)
                    {
                        PictureName = "/Sprites/PsiZetaFront1.png";
                        front = 2;
                    }
                    else if (front == 2)
                    {
                        PictureName = "/Sprites/PsiZetaFront.png";
                        front = 3;
                    }
                    else
                    {
                        PictureName = "/Sprites/PsiZetaFront2.png";
                        front = 0;
                    }
                }
            }
            else if (changeInX < 0)
            {
                if (isAttacking)
                {
                    PictureName = "/Sprites/PsiZetaBackWhip.png";
                    isAttacking = false;
                }
                else if (attackTime > 5)
                {
                    if (back == 0)
                    {
                        PictureName = "/Sprites/PsiZetaBack.png";
                        back = 1;
                    }
                    else if (back == 1)
                    {
                        PictureName = "/Sprites/PsiZetaBack1.png";
                        back = 2;
                    }
                    else if (back == 20)
                    {
                        PictureName = "/Sprites/PsiZetaBack.png";
                        back = 3;
                    }
                    else
                    {
                        PictureName = "/Sprites/PsiZetaBack2.png";
                        back = 0;
                    }
                }
            }
        }
        public void ChangeYFrames(int changeInX)
        {
            if (changeInX > 0)
            {
                if (isAttacking)
                {
                    PictureName = "/Sprites/PsiZetaRightWhip.png";
                    isAttacking = false;
                }
                else if (attackTime > 5)
                {
                    if (right == 0)
                    {
                        PictureName = "/Sprites/PsiZetaRight.png";
                        right = 1;
                    }
                    else if (right == 1)
                    {
                        PictureName = "/Sprites/PsiZetaRight1.png";
                        right = 2;
                    }
                    else if (right == 2)
                    {
                        PictureName = "/Sprites/PsiZetaRight.png";
                        right = 3;
                    }
                    else
                    {
                        PictureName = "/Sprites/PsiZetaRight2.png";
                        right = 0;
                    }
                }

            }
            else if (changeInX < 0)
            {
                if (isAttacking)
                {
                    PictureName = "/Sprites/PsiZetaLeftWhip.png";
                    isAttacking = false;
                }
                else if (attackTime > 5)
                {
                    if (left == 0)
                    {
                        PictureName = "/Sprites/PsiZetaLeft.png";
                        left = 1;
                    }
                    else if (left == 1)
                    {
                        PictureName = "/Sprites/PsiZetaLeft1.png";
                        left = 2;
                    }
                    else if (left == 20)
                    {
                        PictureName = "/Sprites/PsiZetaLeft.png";
                        left = 3;
                    }
                    else
                    {
                        PictureName = "/Sprites/PsiZetaLeft2.png";
                        left = 0;
                    }
                }
            }
        }

        public void PatrolVert(Game game)
        {
            vertCount++;
            if (vertDirection == "north")
            {
                if (AIIsNotWall(game.CurBuilding, 0, -changeNum))
                {
                    Y -= changeNum;
                    ChangeXFrames(-changeNum);
                }
                else
                {
                    Y += changeNum;
                    horizDirection = "south";
                    ChangeXFrames(changeNum);
                }
            }
            else if (horizDirection == "south")
            {
                if (AIIsNotWall(game.CurBuilding, 0, changeNum))
                {
                    Y += changeNum;
                    ChangeXFrames(changeNum);
                }
                else
                {
                    Y -= changeNum;
                    horizDirection = "north";
                    ChangeXFrames(-changeNum);
                }
            }
            if (vertCount > 30)
            {
                patrolVertically = false;
                vertCount = 0;
            }
        }
        public void PatrolHoriz(Game game)
        {
            horizCount++;
            if (horizDirection  == "west")
            {
                if  (AIIsNotWall(game.CurBuilding, -changeNum, 0))
                {
                    X -= changeNum;
                    ChangeYFrames(-changeNum);
                }
                else
                {
                    X += changeNum;
                    horizDirection = "east";
                    ChangeYFrames(changeNum);
                }
            }
            else if (horizDirection == "east")
            {
                if (AIIsNotWall(game.CurBuilding, changeNum, 0))
                {
                    X += changeNum;
                    ChangeYFrames(changeNum);
                }
                else
                {
                    X -= changeNum;
                    horizDirection = "west";
                    ChangeYFrames(-changeNum);
                }
            }
            if (horizCount > 25)
            {
                patrolVertically = true;
                horizCount = 0;
            }
        }
        public void Patrol(Building building)
        {
            int direc = 0;
            vertCount++;
            if (vertCount > 10)
            {
                Random rand = new Random();
                direc = rand.Next(4);
            }
            if (direc == 0)
            {
                //west
                if (AIIsNotWall(building, -changeNum / 2, 0))
                {
                    X -= changeNum;
                    ChangeYFrames(-changeNum);
                }
                else
                {
                    X += changeNum;
                    ChangeYFrames(changeNum);
                }
            }
            else if (direc == 1)
            {
                //east
                if (AIIsNotWall(building, (changeNum / 2), 0))
                {
                    X += changeNum;
                    ChangeYFrames(changeNum);
                }
                else
                {
                    X -= changeNum;
                    ChangeYFrames(-changeNum);
                }
            }
            else if (direc == 2)
            {
                //north
                if (AIIsNotWall(building, 0, (-changeNum / 2)))
                {
                    Y -= changeNum;
                    ChangeXFrames(-changeNum);
                }
                else
                {
                    Y += changeNum;
                    ChangeXFrames(changeNum);
                }
            }
            else if (direc == 3)
            {
                //south
                if (AIIsNotWall(building, 0, (changeNum / 2)))
                {
                    Y += changeNum / 2;
                    ChangeXFrames(changeNum);
                }
                else
                {
                    Y -= changeNum / 2;
                    ChangeXFrames(-changeNum);
                }
            }
        }

        public void Chase(Game game)
        {
            if (game.Marcus.X < X)
            {
                if (IsNotWall(game.CurBuilding, (-changeNum / 2), 0))
                {
                    X -= changeNum / 2;
                    ChangeYFrames(-changeNum / 2);
                }
            }
            else if (game.Marcus.X > X)
            {
                if (IsNotWall(game.CurBuilding, (changeNum / 2), 0))
                {
                    X += changeNum / 2;
                    ChangeYFrames(changeNum / 2);
                }
            }
            if (game.Marcus.Y < Y)
            {
                if (IsNotWall(game.CurBuilding, 0, (-changeNum / 2)))
                {
                    Y -= changeNum / 2;
                    ChangeXFrames(-changeNum / 2);
                }
            }
            else if (game.Marcus.Y > Y)
            {
                if (IsNotWall(game.CurBuilding,0, (changeNum / 2)))
                {
                    Y += changeNum / 2;
                    ChangeXFrames(changeNum / 2);
                }
            }
        }

        public void Move(Game game)
        {
            if (Game.isPaused != true)
            {
                if ((400 * 400) >= (((X - game.Marcus.X) * (X - game.Marcus.X)) + ((Y - game.Marcus.Y) * (Y - game.Marcus.Y))))
                {
                    Chase(game);
                    if ((150 * 150) >= (((X - game.Marcus.X) * (X - game.Marcus.X)) + ((Y - game.Marcus.Y) * (Y - game.Marcus.Y))))
                    {
                        if (game.IsCheatOn != true)
                        {
                            attackTime++;
                            if (attackTime > 10)
                            {
                                isAttacking = true;
                                attackTime = 0;
                            }
                        }
                    }
                    else
                        attackTime = 10;
                }
                else
                {
                    //if (patrolVertically)
                    //    PatrolVert(game);
                    //else
                    //{
                    //    PatrolHoriz(game);
                    //} 
                    Patrol(game.CurBuilding);
                }
            }
        }

        public string Serialize()
        {
            return string.Format("AI?5,HEALTH!{0},DAMAGE!{1},SPEED!{2},X!{3},Y!{4}", Health, Damage, Speed, X, Y);
        }

        public AI Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));

            for (int i = 0; i < properties.Count; i += 2)
            {
                switch (properties[i])
                {
                    case "HEALTH":
                        Health = int.Parse(properties[i + 1]);
                        break;
                    case "DAMAGE":
                        Damage = int.Parse(properties[i + 1]);
                        break;
                    case "SPEED":
                        Speed = int.Parse(properties[i + 1]);
                        break;
                    case "X":
                        X = int.Parse(properties[i + 1]);
                        break;
                    case "Y":
                        Y = int.Parse(properties[i + 1]);
                        break;
                }
            }

            return this;
        }
    }
}
