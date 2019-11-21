using CalculationTools.Common;
using System.Collections.Generic;
using System.Diagnostics;

namespace CalculationTools.Core
{
    public class VillageSelectorViewModel : BaseViewModel
    {
        public Village SelectedVillage { get; set; }
        public int SelectedVillageId { get; set; }
        public List<Village> VillageOptions { get; set; } = new List<Village> {
             new Village {
              Id = 597,
               Name = "A01 FAKENAME Alpha",
               X = 517,
               Y = 488,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 930,
               Name = "A02 FAKENAME Beta",
               X = 518,
               Y = 488,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 515,
               Name = "A03 FAKENAME Gamma",
               X = 516,
               Y = 487,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 757,
               Name = "A05 FAKENAME Epsilon",
               X = 514,
               Y = 487,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 603,
               Name = "A06 FAKENAME Zeta",
               X = 514,
               Y = 488,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 424,
               Name = "A07 FAKENAME Eta",
               X = 524,
               Y = 488,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 657,
               Name = "A08 FAKENAME Theta",
               X = 523,
               Y = 487,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 774,
               Name = "A09 FAKENAME Iota",
               X = 524,
               Y = 486,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1423,
               Name = "B01 FAKENAME Kappa",
               X = 526,
               Y = 480,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1626,
               Name = "B02 FAKENAME Tau",
               X = 532,
               Y = 486,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 2694,
               Name = "Barbarendorp",
               X = 538,
               Y = 486,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1091,
               Name = "C01 FAKENAME Lamda",
               X = 529,
               Y = 496,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1040,
               Name = "C02 FAKENAME Omicron",
               X = 528,
               Y = 490,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1292,
               Name = "C03 FAKENAME Pi",
               X = 530,
               Y = 492,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1512,
               Name = "C04 FAKENAME Chi",
               X = 529,
               Y = 493,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1335,
               Name = "C05 FAKENAME Psi",
               X = 529,
               Y = 491,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 856,
               Name = "C06 FAKENAME",
               X = 527,
               Y = 492,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1756,
               Name = "C07 FAKENAME",
               X = 533,
               Y = 493,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1503,
               Name = "C08 FAKENAME",
               X = 533,
               Y = 495,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1711,
               Name = "C09 FAKENAME",
               X = 530,
               Y = 497,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1517,
               Name = "C10 FAKENAME",
               X = 534,
               Y = 489,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1680,
               Name = "C11 FAKENAME",
               X = 535,
               Y = 491,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1765,
               Name = "C12 FAKENAME",
               X = 536,
               Y = 493,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1755,
               Name = "C13 FAKENAME B",
               X = 536,
               Y = 495,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1084,
               Name = "C14 FAKENAME",
               X = 530,
               Y = 489,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1509,
               Name = "C15 FAKENAME",
               X = 533,
               Y = 494,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1518,
               Name = "C16 FAKENAME",
               X = 538,
               Y = 493,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1332,
               Name = "C17 FAKENAME",
               X = 534,
               Y = 496,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1016,
               Name = "C18 FAKENAME",
               X = 529,
               Y = 492,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 716,
               Name = "C19 FAKENAME",
               X = 528,
               Y = 494,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 761,
               Name = "D01 FAKENAME Mu",
               X = 518,
               Y = 493,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 370,
               Name = "D02 FAKENAME Nu",
               X = 518,
               Y = 494,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 348,
               Name = "D03 FAKENAME Xi",
               X = 515,
               Y = 494,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 367,
               Name = "D04 FAKENAME",
               X = 518,
               Y = 498,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1298,
               Name = "D05 FAKENAME",
               X = 523,
               Y = 498,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 238,
               Name = "D06 FAKENAME",
               X = 522,
               Y = 499,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 368,
               Name = "D07 FAKENAME",
               X = 521,
               Y = 495,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 372,
               Name = "D08 FAKENAME",
               X = 522,
               Y = 496,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 334,
               Name = "D09 FAKENAME",
               X = 523,
               Y = 491,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1496,
               Name = "E01 Kom dan!!!",
               X = 520,
               Y = 475,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 406,
               Name = "F01 FAKENAME",
               X = 511,
               Y = 484,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1530,
               Name = "G01 FAKENAME",
               X = 547,
               Y = 499,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1521,
               Name = "G02 FAKENAME",
               X = 548,
               Y = 500,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 2116,
               Name = "G03 FAKENAME",
               X = 549,
               Y = 500,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1759,
               Name = "G04 FAKENAME",
               X = 547,
               Y = 497,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 2118,
               Name = "G05 FAKENAME",
               X = 549,
               Y = 499,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 1591,
               Name = "G06 FAKENAME",
               X = 543,
               Y = 498,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 2112,
               Name = "G07 FAKENAME",
               X = 539,
               Y = 499,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 2378,
               Name = "G08 FAKENAME",
               X = 540,
               Y = 497,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 2119,
               Name = "G09 FAKENAME",
               X = 541,
               Y = 496,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 2113,
               Name = "G10 FAKENAME",
               X = 541,
               Y = 495,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 2115,
               Name = "G11 FAKENAME",
               X = 541,
               Y = 501,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 2379,
               Name = "G12 FAKENAME",
               X = 541,
               Y = 499,
               CharacterId = 999888,
               WorldId = "nl37"
             },
             new Village {
              Id = 2236,
               Name = "H01 FAKENAME",
               X = 533,
               Y = 474,
               CharacterId = 999888,
               WorldId = "nl37"
             }
            };

        private string _searchText = string.Empty;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Debug.WriteLine($"{value}");
                OnPropertyChanged();
            }
        }

    }
}
