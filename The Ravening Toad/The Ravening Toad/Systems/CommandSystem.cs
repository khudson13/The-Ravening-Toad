using RaveningToad;
using RLNET;
using RogueSharp;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.IO;
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

            bool hits = ResolveAttack(attacker, defender, attackMessage);

            //int blocks = ResolveDefense(defender, hits, attackMessage, defenseMessage);

            Game.MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString()))
            {
                Game.MessageLog.Add(defenseMessage.ToString());
            }

            //int damage = hits - blocks;
            
            ResolveDamage(defender, hits, attacker.damage);
            
        }

        private static bool ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            //int hits = 0;

            attackMessage.AppendFormat("{0} attacks {1}", attacker.Name, defender.Name);

            // Roll attack value
            DiceExpression attackDice = new DiceExpression().Dice(1, attacker.Attack);
            DiceResult attackResult = attackDice.Roll();

            // Roll defense value
            DiceExpression defenseDice = new DiceExpression().Dice(1, defender.Defense);
            DiceResult defenseResult = defenseDice.Roll();

            return attackResult.Value >= defenseResult.Value;
        }

        // Apply damage to the defender
        public static void ResolveDamage(Actor defender, bool hits, int damage)
        {
            if (hits)
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
                Game.MessageLog.Add($"  {defender.Name} avoided all damage");
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

                Game.Player.Meat += defender.Meat;
            }
        }
    }
}
