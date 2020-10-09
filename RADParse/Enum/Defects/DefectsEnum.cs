using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RADParse.Enum.Defects
{
    /// <summary>
    /// The enum contains type of road defect and its number in TKP
    /// </summary>
    public enum DefectsEnum
    {
        Track_1_3_1 = 2, // колея
        Track_1_3_2, // колея
        Track_1_3_3, // колея
        Track_1_3_4, // колея
        Track_1_3_5, // колея
        Track_1_4_1, // колея
        Track_1_4_2, // колея
        Track_1_4_3, // колея
        Track_1_4_4, // колея
        Track_1_4_5, // колея
        Sweating_1_8_1 = 18, // выпотевание
        Sweating_1_9_1, // выпотевание
        Potholes_1_10_1, // выбоины
        Potholes_1_10_2, // выбоины
        Patches_1_14_1 = 27, // "заплаты"
        IrregularitiesPatch_1_15_1, // неровности "заплаты"
        IrregularitiesPatch_1_15_2, // неровности "заплаты"
        Peeling_1_16_1, // шелушение
        Chipping_1_17_1, // выкрашивание
        IndividualCracks_1_18_1, // отдельные трещины
        IndividualCracks_1_18_2, // отдельные трещины
        IndividualCracks_1_18_3, // отдельные трещины
        RareCracks_1_19_1, // редкие трещины
        RareCracks_1_19_2, // редкие трещины
        RareCracks_1_19_3, // редкие трещины
        FrequentCracks_1_21_1 = 39, // частые трещины
        FrequentCracks_1_21_2, // частые трещины
        FrequentCracks_1_21_3, // частые трещины
        CrackMesh_1_23_1 = 43, // сетка трещин
        CrackMesh_1_23_2, // сетка трещин
        CrackMesh_1_23_3, // сетка трещин
        CrackMesh_1_25_1 = 47, // сетка трещин
        CrackMesh_1_25_2, // сетка трещин
        CrackMesh_1_25_3, // сетка трещин
        DestructionOfSeams_1_27_1 = 51, // разрушение швов
        CurbDefects_1_30_1 = 54, // дефекты бортового камня
        TechnologicalDefect_1_31_1, // технологический дефект, связанный с некачественной укладкой и уплотнением
        TechnologicalDefect_1_32_1, // технологический дефект, связанный с некачественной укладкой и уплотнением
        TechnologicalDefect_1_33_1, // технологический дефект, связанный с некачественной укладкой и уплотнением
        TechnologicalDefect_1_34_1 // технологический дефект, связанный с некачественной укладкой и уплотнением
    }
}