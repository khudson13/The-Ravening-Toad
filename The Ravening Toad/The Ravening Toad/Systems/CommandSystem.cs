﻿using RaveningToad;
using RogueSharp;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using The_Ravening_Toad.Interfaces;

namespace The_Ravening_Toad.Systems
{
    public class CommandSystem
    {

        public void ActivateMonsters()
        {
            IScheduleable scheduleable = Game.SchedulingSystem.Get();
            if (scheduleable is Player)
            {
                IsPlayerTurn = true;
                Game.SchedulingSystem.Add(Game.Player);
            }
            else
            {
                Monster monster = scheduleable as Monster;

                if (monster != null)
                {
                    monster.PerformAction(this);
                    Game.SchedulingSystem.Add(monster);
                }

                ActivateMonsters();
            }
        }

        public void EndPlayerTurn()
        {
            IsPlayerTurn = false;
        }

        public bool IsPlayerTurn { get; set; }

        public void MoveMonster(Monster monster, ICell cell)
        {
            if (!Game.ToadMap.SetActorPosition(monster, cell.X, cell.Y))
            {
                if (Game.Player.X == cell.X && Game.Player.Y == cell.Y)
                {
                    Attack(monster, Game.Player);
                }
            }
        }

        // Returns true if player moved
        // false if blocked
        public bool MovePlayer(Direction direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;

            switch (direction)
            {
                case Direction.Up:
                    {
                        y = Game.Player.Y - 1;
                        break;
                    }
                case Direction.Down:
                    {
                        y = Game.Player.Y + 1;
                        break;
                    }
                case Direction.Left:
                    {
                        x = Game.Player.X - 1;
                        break;
                    }
                case Direction.Right:
                    {
                        x = Game.Player.X + 1;
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }

            Monster monster = Game.ToadMap.GetMonsterAt(x, y);

            if (monster != null)
            {
                Attack(Game.Player, monster);
                return true;
            }

            return Game.ToadMap.SetActorPosition(Game.Player, x, y);
        }

        public void Attack(Actor attacker, Actor defender)
        {
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            int hits = ResolveAttack(attacker, defender, attackMessage);

            int blocks = ResolveDefense(defender, hits, attackMessage, defenseMessage);

            Game.MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString()))
            {
                Game.MessageLog.Add(defenseMessage.ToString());
            }

            int damage = hits - blocks;

            ResolveDamage(defender, damage);
        }


        private static int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            int hits = 0;

            attackMessage.AppendFormat("{0} attacks {1} and rolls: ", attacker.Name, defender.Name);

            // Roll a number of 100-sided dice equal to the Attack value of the attacking actor
            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            // Look at the face value of each single die that was rolled
            foreach (TermResult termResult in attackResult.Results)
            {
                attackMessage.Append(termResult.Value + ", ");
                // Compare the value to 100 minus the attack chance and add a hit if it's greater
                if (termResult.Value >= 100 - attacker.AttackChance)
                {
                    hits++;
                }
            }

            return hits;
        }


        private static int ResolveDefense(Actor defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int blocks = 0;

            if (hits > 0)
            {
                attackMessage.AppendFormat("scoring {0} hits.", hits);
                defenseMessage.AppendFormat("  {0} defends and rolls: ", defender.Name);

                // Roll a number of 100-sided dice equal to the Defense value of the defendering actor
                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();

                // Look at the face value of each single die that was rolled
                foreach (TermResult termResult in defenseRoll.Results)
                {
                    defenseMessage.Append(termResult.Value + ", ");
                    // Compare the value to 100 minus the defense chance and add a block if it's greater
                    if (termResult.Value >= 100 - defender.DefenseChance)
                    {
                        blocks++;
                    }
                }
                defenseMessage.AppendFormat("resulting in {0} blocks.", blocks);
            }
            else
            {
                attackMessage.Append("and misses completely.");
            }

            return blocks;
        }

        // Apply any damage that wasn't blocked to the defender
        private static void ResolveDamage(Actor defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;

                Game.MessageLog.Add($"  {defender.Name} was hit for {damage} damage");

                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
            else
            {
                Game.MessageLog.Add($"  {defender.Name} blocked all damage");
            }
        }

        // Remove the defender from the map and add some messages upon death.
        private static void ResolveDeath(Actor defender)
        {
            if (defender is Player)
            {
                Game.MessageLog.Add($"  {defender.Name} was killed, GAME OVER MAN!");
            }
            else if (defender is Monster)
            {
                Game.ToadMap.RemoveMonster((Monster)defender);

                Game.MessageLog.Add($"  {defender.Name} died leaving {defender.Meat} delicious(?) meat");
            }
        }
    }
}
