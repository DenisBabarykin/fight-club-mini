using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public static class StringToBodyPartConverter
{
    public static BodyParts Convert(string bodyPart)
    {
        BodyParts parts = BodyParts.None;
        string bodyPartStr = bodyPart.ToLower();

        if (bodyPartStr.Contains("head"))
        {
            parts |= BodyParts.Head;
        }

        if (bodyPartStr.Contains("body"))
        {
            parts |= BodyParts.Body;
        }

        if (bodyPartStr.Contains("groin"))
        {
            parts |= BodyParts.Groin;
        }

        if (bodyPartStr.Contains("legs"))
        {
            parts |= BodyParts.Legs;
        }

        return parts;
    }
}