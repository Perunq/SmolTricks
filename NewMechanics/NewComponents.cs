using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SmolCraft.NewMechanics
{
    class NewComponents
    {
        [AllowMultipleComponents]
        [TypeId("eb2b79eff9f84158b2556e9b67000000")]
        public class MundaneBeatingComponent : EntityFactComponentDelegate<AddInitiatorAttackWithWeaponTrigger.ComponentData>, IInitiatorRulebookHandler<RulePrepareDamage>, IRulebookHandler<RulePrepareDamage>, ISubscriber, IInitiatorRulebookSubscriber
        {
           
            public void OnEventAboutToTrigger(RulePrepareDamage evt)
            {
                Size GetBaseDiceSize(RulePrepareDamage evt)
                {
                    Size result = Size.Medium;
                    
                    if (!evt.ParentRule.AttackRoll.Initiator.Body.IsPolymorphed)
                    {
                        return result;
                    }
                    Polymorph component = evt.ParentRule.AttackRoll.Initiator.GetActivePolymorph().Component;
                    if (component != null && component.UseSizeAsBaseForDamage)
                    {
                        return component.Size;
                    }
                    return result;
                }

                RuleAttackRoll AttackRoll = evt.ParentRule.AttackRoll;
                if (AttackRoll is null)
                {
                    return;
                }
                if (!AttackRoll.IsHit) 
                {
                    return;
                }
                if (AttackRoll.IsCriticalConfirmed)
                {
                    return;
                }
                DiceFormula newdiceformula = WeaponDamageScaleTable.Scale(
                    evt.DamageBundle.WeaponDamage.Dice.BaseFormula, (evt.DamageBundle.Weapon.Size)+1,GetBaseDiceSize(evt),evt.DamageBundle.Weapon.Blueprint);
                //, (((int)evt.DamageBundle.Weapon.Size + 1)== 8)? (evt.DamageBundle.Weapon.Size + 1) : Size.Colossal);


                //BaseDamage damage = new DamageDescription
                //{
                //    TypeDescription = DamageTypes.Physical(),
                //    Dice = newdiceformula,
                //    Bonus = 0,
                //    SourceFact = base.Fact
                //}.CreateDamage();



                //evt.Add(damage);
                evt.DamageBundle.WeaponDamage.Dice.Modify(newdiceformula,ModifierDescriptor.Size);
            }

        


            public void OnEventDidTrigger(RulePrepareDamage evt)
            {
               
            }
        }



        [AllowMultipleComponents]
        [TypeId("eb2b79eff9f84158b2556e9b67000000")]
        public class RakeExtraAttackComponent : EntityFactComponentDelegate<AddInitiatorAttackRollTrigger>, IInitiatorRulebookHandler<RuleAttackRoll>, IRulebookHandler<RuleAttackRoll>, ISubscriber, IInitiatorRulebookSubscriber
        {
         
            public void OnEventAboutToTrigger(RuleAttackRoll evt)
            {
                throw new NotImplementedException();
            }

            public void OnEventDidTrigger(RuleAttackRoll evt)
            {
                throw new NotImplementedException();
            }
        }

    }
}
