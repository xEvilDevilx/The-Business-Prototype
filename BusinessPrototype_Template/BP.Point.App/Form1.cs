using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BP.Point.App.Properties;
using BP.Point.Data.Layout;
using BP.SDK.Interfaces.Serialize;
using BP.SDK.Serialize;
using BP.Visual.Base.Enums;

namespace BP.Point.App
{
    public partial class Form1 : Form
    {
        private int _currentScreenWidth;
        private int _currentScreenHeight;
        private PointLayoutData _layout;
        private LayoutManager _layoutManager;

        public Form1()
        {
            InitializeComponent();

            var dir = Directory.GetCurrentDirectory();
            var fileName = "PointLayout.lay";
            var filePath = string.Concat(dir, "\\", fileName);

            _currentScreenWidth = this.Size.Width;
            _currentScreenHeight = this.Size.Height;

            Theme cmnTheme = Theme.Blue;
            switch (cmnTheme)
            {
                case Theme.Blue:
                    this.BackColor = Color.FromArgb(78, 145, 238);
                    break;
                case Theme.Green:
                    this.BackColor = Color.FromArgb(112, 196, 113);
                    break;
                case Theme.Red:
                    this.BackColor = Color.FromArgb(255, 107, 75);
                    break;
                case Theme.Yellow:
                    this.BackColor = Color.FromArgb(255, 201, 48);
                    break;
                case Theme.Pink:
                    this.BackColor = Color.FromArgb(215, 46, 117);
                    break;
            }

            //if (!File.Exists(filePath))
            {
                // Areas ID:
                // 0 - Scan
                // 1 - Shares
                // 2 - Settings
                // 3 - Product Info

                int cmnLblOutlineWidth = 6;
                int cmnLblWidth = 900;
                int cmnLblHeight = 150;
                float cmnLblFontSize = 50f;

                int cmnBtnWidth = 200;
                int cmnBtnHeight = 75;
                float cmnBtnFontSize = 18f;
                int cmnBtnWhitespaceInWidth = 40;
                int cmnBtnWhitespaceFromBottom = 30;

                int imgProductPicWidth = 600;
                int imgProductPicHeight = 390;
                int intProductInfoWhitespaceWidth = 10;
                int txtProductInfoWidth = 360;
                int txtProductInfoHeight = 390;

                int rightProductInfoBarWidth = 220;
                int rightProductInfoCaptionHeight = 50;
                int rightProductInfoValueHeight = 70;
                int rightProductInfoWhitespaceHeight = 10;
                float rightProductInfoCaptionFontSize = 30f;
                float rightProductInfoValueFontSize = 18f;
                int rightProductInfoTextOutlineWidth = 3;

                int sharesDescriptionWidth = 1000;
                int sharesDescriptionHeight = 370;
                int sharesBtnSmallWidth = 100;
                int sharesBtnSmallHeight = 75;

                var imgStore = Resources.TestStoreImage;

                int productID = 2;
                Bitmap imgProduct;
                string productText = "";
                string productPrice = "";
                string productArticle = "";
                string productBarcode = "";
                string productLabel = "";
                switch (productID)
                {
                    case 0:
                        imgProduct = Resources.BossWoman;
                        productLabel = "Hugo Boss Woman";
                        productText = "Hugo Boss Woman — настоящий подарок для элегантных, уверенных в себе современных женщин. Аромат Boss наполнен силой и энергией окружающего мира, его яркость и насыщенность соседствует с легкостью и утонченностью";
                        productPrice = "4 290 Р";
                        productArticle = "HU001LWNUK80";
                        productBarcode = "80876548";
                        break;
                    case 1:
                        imgProduct = Resources.Amouage_Honour;
                        productLabel = "Amouage Honour women";
                        productText = "Amouage Honour women («Амуаж. Женщина Чести») - это аромат для женщин. Симфония аромата станет идеальной гармонией во время рабочего дня и романтического вечера. Женский парфюм Amouage Honour Women обладает пряным цветочным ароматом, с роскошным и чувственным восточным шлейфом";
                        productPrice = "12 755 Р";
                        productArticle = "49829";
                        productBarcode = "61175992";
                        break;
                    case 2:
                        imgProduct = Resources.MAN_in_RED;
                        productLabel = "\"Man in Red\" от Ferrari Cavallino";
                        productText = "Мужская туалетная вода \"Man in Red\" от Ferrari Cavallino - это аромат, принадлежащий к группе ароматов фужерные. Верхние ноты - бергамот, кардамон и красное яблоко; ноты сердца - желтая слива, апельсиновый цвет и лаванда; ноты базы - лабданум, белый кедр и тонка бобы. Объем 50 мл";
                        productPrice = "3 140 Р";
                        productArticle = "FE019LMHLJ15 ";
                        productBarcode = "15549853";
                        break;
                    default:
                        imgProduct = Resources.BossWoman;
                        productLabel = "Hugo Boss Woman";
                        productText = "Hugo Boss Woman — настоящий подарок для элегантных, уверенных в себе современных женщин. Аромат Boss наполнен силой и энергией окружающего мира, его яркость и насыщенность соседствует с легкостью и утонченностью";
                        productPrice = "4 290 Р";
                        productArticle = "HU001LWNUK80";
                        productBarcode = "80876548";
                        break;
                }

                int promotionsID = 0;
                string promotionLabel = "";
                string promotionText = "";
                string promotionBtnUp = "";
                string promotionBtnDown = "";
                string promotionBtnBack = "";
                string promotionBtnSettings = "";
                switch(promotionsID)
                {
                    case 0:
                        promotionLabel = "\"Пробник в Подарок\"";
                        promotionText = "Любой покупатель, сделавший заказ на сумму от 3 000 рублей, получает в подарок пробник аромата из категории «Хит месяца» или «Новинки магазина». При последующей покупке этого аромата покупатель получает скидку в размере 3%";
                        promotionBtnUp = "Вверх";
                        promotionBtnDown = "Вниз";
                        promotionBtnBack = "Назад";
                        promotionBtnSettings = "Настройки";
                        break;
                    case 1:
                        promotionLabel = "\"探索作为礼物\"";
                        promotionText = "任何订单金额达到3,000卢布或以上的买家都会收到“每月点击”或“商店新商品”类别的香水采样器。在随后购买此香水后，买家可享受3％的折扣。";
                        promotionBtnUp = "向上";
                        promotionBtnDown = "下";
                        promotionBtnBack = "前";
                        promotionBtnSettings = "设置";
                        break;
                    default:
                        promotionLabel = "\"Пробник в Подарок\"";
                        promotionText = "Любой покупатель, сделавший заказ на сумму от 3 000 рублей, получает в подарок пробник аромата из категории «Хит месяца» или «Новинки магазина». При последующей покупке этого аромата покупатель получает скидку в размере 3%";
                        promotionBtnUp = "Вверх";
                        promotionBtnDown = "Вниз";
                        promotionBtnBack = "Назад";
                        promotionBtnSettings = "Настройки";
                        break;
                }
                
                #region Scan Screen

                var lblScanPleaseText = new PointLayoutItem()
                {
                    BackColor = Color.FromArgb(255, 58, 125, 218),
                    ItemID = 0,
                    ItemName = "lblscanPleaseText",
                    ItemType = ComponentTypes.CLabel,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(900, 300),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) - (900 / 2), (_currentScreenHeight / 2) - (300 / 2)),
                    PointFont = new Font(Font.FontFamily, 60f),
                    Text = "Scan product barcode please!",
                    ItemObject = new PointLayoutCLabel()
                    {
                        OutlineAlignment = StringAlignment.Center,
                        OutlineLineAlignment = StringAlignment.Center,
                        OutlineWidth = cmnLblOutlineWidth
                    }
                };
                var imgScanStore = new PointLayoutItem()
                {
                    ItemID = 1,
                    ItemName = "imgScanStore",
                    ItemType = ComponentTypes.Image,
                    Image = imgStore,
                    ImageSizeMode = PictureBoxSizeMode.Zoom,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(300, 150),
                    PointLocation = new System.Drawing.Point(_currentScreenWidth - 300, 0),
                    PointFont = Font
                };
                var btnScanShares = new PointLayoutItem()
                {
                    ItemID = 2,
                    ItemName = "btnScanShares",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 1,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) - cmnBtnWidth - (cmnBtnWhitespaceInWidth / 2) - cmnBtnWidth - cmnBtnWhitespaceInWidth, _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize),
                    Text = "Promotions",
                    ItemObject = new PointLayoutButton()
                    {
                        LoadAreaID = 1
                    }
                };
                var btnScanSettings = new PointLayoutItem()
                {
                    ItemID = 3,
                    ItemName = "btnScanSettings",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 1,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) + cmnBtnWidth + (cmnBtnWhitespaceInWidth / 2) + cmnBtnWhitespaceInWidth, _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize),
                    Text = "Settings",
                    ItemObject = new PointLayoutButton()
                    {
                        LoadAreaID = 3
                    }
                };

                var scanArea = new PointLayoutArea()
                {
                    AreaID = 0,
                    AreaName = "ScanScreen",
                    AreaSize = new Size(_currentScreenWidth, _currentScreenHeight),
                    AreaLocation = new System.Drawing.Point(0, 0),
                    Items = new List<PointLayoutItem>()
                    {
                        lblScanPleaseText,
                        imgScanStore,
                        btnScanShares,
                        btnScanSettings
                    }
                };

                #endregion

                #region Shares Screen

                var lblSharesMainText = new PointLayoutItem()
                {
                    BackColor = Color.FromArgb(255, 58, 125, 218),
                    ItemID = 0,
                    ItemName = "lblSharesMainText",
                    ItemType = ComponentTypes.CLabel,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(cmnLblWidth, cmnLblHeight),
                    PointLocation = new System.Drawing.Point(10, 10),
                    PointFont = new Font(Font.FontFamily, cmnLblFontSize),
                    Text = promotionLabel,
                    ItemObject = new PointLayoutCLabel()
                    {
                        OutlineAlignment = StringAlignment.Center,
                        OutlineLineAlignment = StringAlignment.Center,
                        OutlineWidth = cmnLblOutlineWidth
                    }
                };
                var imgSharesStore = new PointLayoutItem()
                {
                    ItemID = 1,
                    ItemName = "imgSharesStore",
                    ItemType = ComponentTypes.Image,
                    Image = imgStore,
                    ImageSizeMode = PictureBoxSizeMode.Zoom,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(300, 150),
                    PointLocation = new System.Drawing.Point(_currentScreenWidth - 300, 0),
                    PointFont = Font
                };
                var lblSharesDescriptionText = new PointLayoutItem()
                {
                    ItemID = 2,
                    ItemName = "lblSharesDescriptionText",
                    ItemType = ComponentTypes.Label,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(sharesDescriptionWidth, sharesDescriptionHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) - (sharesDescriptionWidth / 2), (_currentScreenHeight / 2) - (sharesDescriptionHeight / 2)),
                    PointFont = new Font(Font.FontFamily, 18f),
                    Text = promotionText
                };
                var btnSharesLeft = new PointLayoutItem()
                {
                    ItemID = 3,
                    ItemName = "btnSharesLeft",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 100,
                    PointSize = new Size(sharesBtnSmallWidth, sharesBtnSmallHeight),
                    PointLocation = new System.Drawing.Point(10, (_currentScreenHeight / 2) - (sharesBtnSmallHeight / 2)),
                    PointFont = new Font(Font.FontFamily, 24f),
                    Text = "<"
                };
                var btnSharesRight = new PointLayoutItem()
                {
                    ItemID = 4,
                    ItemName = "btnSharesRight",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 101,
                    PointSize = new Size(sharesBtnSmallWidth, sharesBtnSmallHeight),
                    PointLocation = new System.Drawing.Point(_currentScreenWidth - sharesBtnSmallWidth - 10, (_currentScreenHeight / 2) - (sharesBtnSmallHeight / 2)),
                    PointFont = new Font(Font.FontFamily, 24f),
                    Text = ">"
                };
                var btnSharesUp = new PointLayoutItem()
                {
                    ItemID = 5,
                    ItemName = "btnSharesUp",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 102,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) - cmnBtnWidth - (cmnBtnWhitespaceInWidth / 2) - cmnBtnWidth - cmnBtnWhitespaceInWidth, _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize),
                    Text = promotionBtnUp
                };
                var btnSharesDown = new PointLayoutItem()
                {
                    ItemID = 6,
                    ItemName = "btnSharesDown",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 103,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) - cmnBtnWidth - (cmnBtnWhitespaceInWidth / 2), _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize),
                    Text = promotionBtnDown
                };
                var btnSharesBack = new PointLayoutItem()
                {
                    ItemID = 7,
                    ItemName = "btnSharesBack",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 1,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) + (cmnBtnWhitespaceInWidth / 2), _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize),
                    Text = promotionBtnBack,
                    ItemObject = new PointLayoutButton()
                    {
                        LoadAreaID = 0
                    }
                };
                var btnSharesSettings = new PointLayoutItem()
                {
                    ItemID = 8,
                    ItemName = "btnSharesSettings",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 1,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) + cmnBtnWidth + cmnBtnWhitespaceInWidth, _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize),
                    Text = promotionBtnSettings,
                    ItemObject = new PointLayoutButton()
                    {
                        LoadAreaID = 2
                    }
                };

                var sharesArea = new PointLayoutArea()
                {
                    AreaID = 1,
                    AreaName = "SharesScreen",
                    AreaSize = new Size(_currentScreenWidth, _currentScreenHeight),
                    AreaLocation = new System.Drawing.Point(0, 0),
                    Items = new List<PointLayoutItem>()
                    {
                        lblSharesMainText,
                        imgSharesStore,
                        lblSharesDescriptionText,
                        btnSharesLeft,
                        btnSharesRight,
                        btnSharesUp,
                        btnSharesDown,
                        btnSharesBack,
                        btnSharesSettings
                    }
                };

                #endregion

                #region Settings Screen

                var lblSettingsText = new PointLayoutItem()
                {
                    BackColor = Color.FromArgb(255, 58, 125, 218),
                    ItemID = 0,
                    ItemName = "lblSettingsText",
                    ItemType = ComponentTypes.CLabel,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(cmnLblWidth, cmnLblHeight),
                    PointLocation = new System.Drawing.Point(10, 10),
                    PointFont = new Font(Font.FontFamily, cmnLblFontSize),
                    ItemObject = new PointLayoutCLabel()
                    {
                        OutlineAlignment = StringAlignment.Center,
                        OutlineLineAlignment = StringAlignment.Center,
                        OutlineWidth = cmnLblOutlineWidth
                    }
                };
                var imgSettingsStore = new PointLayoutItem()
                {
                    ItemID = 1,
                    ItemName = "imgSettingsStore",
                    ItemType = ComponentTypes.Image,
                    Image = imgStore,
                    ImageSizeMode = PictureBoxSizeMode.Zoom,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(300, 150),
                    PointLocation = new System.Drawing.Point(_currentScreenWidth - 300, 0),
                    PointFont = Font
                };
                var btnSettingsCancel = new PointLayoutItem()
                {
                    ItemID = 2,
                    ItemName = "btnSettingsCancel",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 2,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) - cmnBtnWidth - (cmnBtnWhitespaceInWidth / 2) - cmnBtnWidth - cmnBtnWhitespaceInWidth, _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize)
                };
                var btnSettingsCurrency = new PointLayoutItem()
                {
                    ItemID = 3,
                    ItemName = "btnSettingsCurrency",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 104,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) - cmnBtnWidth - (cmnBtnWhitespaceInWidth / 2), _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize)
                };
                var btnSettingsLanguage = new PointLayoutItem()
                {
                    ItemID = 4,
                    ItemName = "btnSettingsLanguage",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 105,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) + (cmnBtnWhitespaceInWidth / 2), _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize)
                };
                var btnSettingsChoose = new PointLayoutItem()
                {
                    ItemID = 5,
                    ItemName = "btnSettingsChoose",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 106,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) + cmnBtnWidth + cmnBtnWhitespaceInWidth, _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize)
                };

                var settingsArea = new PointLayoutArea()
                {
                    AreaID = 2,
                    AreaName = "SettingsScreen",
                    AreaSize = new Size(_currentScreenWidth, _currentScreenHeight),
                    AreaLocation = new System.Drawing.Point(0, 0),
                    Items = new List<PointLayoutItem>()
                    {
                        lblSettingsText,
                        imgSettingsStore,
                        btnSettingsCancel,
                        btnSettingsCurrency,
                        btnSettingsLanguage,
                        btnSettingsChoose
                    }
                };

                #endregion

                #region Product Info Screen

                var lblProductInfoText = new PointLayoutItem()
                {
                    ItemID = 0,
                    ItemName = "lblProductInfoText",
                    ItemType = ComponentTypes.CLabel,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(cmnLblWidth, cmnLblHeight),
                    PointLocation = new System.Drawing.Point(10, 10),
                    PointFont = new Font(Font.FontFamily, cmnLblFontSize),
                    Text = productLabel,
                    ItemObject = new PointLayoutCLabel()
                    {
                        OutlineAlignment = StringAlignment.Center,
                        OutlineLineAlignment = StringAlignment.Center,
                        OutlineWidth = cmnLblOutlineWidth
                    }
                };
                var imgProductInfoStore = new PointLayoutItem()
                {
                    ItemID = 1,
                    ItemName = "imgProductInfoStore",
                    ItemType = ComponentTypes.Image,
                    Image = imgStore,
                    ImageSizeMode = PictureBoxSizeMode.Zoom,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(300, 150),
                    PointLocation = new System.Drawing.Point(_currentScreenWidth - 300, 0),
                    PointFont = Font
                };                
                var imgProductInfoPicture = new PointLayoutItem()
                {
                    ItemID = 2,
                    ItemName = ItemsConstants.ProductInfoPictureName,
                    ItemType = ComponentTypes.Image,
                    Image = imgProduct,
                    ImageSizeMode = PictureBoxSizeMode.Zoom,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(imgProductPicWidth, imgProductPicHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) - imgProductPicWidth, (_currentScreenHeight / 2) - (imgProductPicHeight / 2)),
                    PointFont = Font
                };
                var lblProductInfoDescriptionText = new PointLayoutItem()
                {
                    ItemID = 3,
                    ItemName = "lblProductInfoDescriptionText",
                    ItemType = ComponentTypes.Label,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(txtProductInfoWidth, txtProductInfoHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) + intProductInfoWhitespaceWidth, (_currentScreenHeight / 2) - txtProductInfoHeight / 2),
                    PointFont = new Font(Font.FontFamily, 18f),
                    Text = productText
                };

                var lblProductInfoPriceText = new PointLayoutItem()
                {
                    ItemID = 4,
                    ItemName = "lblProductInfoPriceText",
                    ItemType = ComponentTypes.CLabel,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(rightProductInfoBarWidth, rightProductInfoCaptionHeight),
                    PointLocation = new System.Drawing.Point(
                        (_currentScreenWidth / 2) + intProductInfoWhitespaceWidth + txtProductInfoWidth + intProductInfoWhitespaceWidth,
                        (_currentScreenHeight / 2) - ((rightProductInfoCaptionHeight + rightProductInfoValueHeight) / 2) - (rightProductInfoCaptionHeight + rightProductInfoValueHeight) - rightProductInfoWhitespaceHeight),
                    PointFont = new Font(Font.FontFamily, rightProductInfoCaptionFontSize),
                    Text = "Price",
                    ItemObject = new PointLayoutCLabel()
                    {
                        OutlineAlignment = StringAlignment.Center,
                        OutlineLineAlignment = StringAlignment.Center,
                        OutlineWidth = rightProductInfoTextOutlineWidth
                    }
                };
                var lblProductInfoPriceValueText = new PointLayoutItem()
                {
                    ItemID = 5,
                    ItemName = "lblProductInfoPriceValueText",
                    ItemType = ComponentTypes.Label,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(rightProductInfoBarWidth, rightProductInfoValueHeight),
                    PointLocation = new System.Drawing.Point(
                        (_currentScreenWidth / 2) + intProductInfoWhitespaceWidth + txtProductInfoWidth + intProductInfoWhitespaceWidth,
                        (_currentScreenHeight / 2) - ((rightProductInfoCaptionHeight + rightProductInfoValueHeight) / 2) + rightProductInfoCaptionHeight - (rightProductInfoCaptionHeight + rightProductInfoValueHeight) - rightProductInfoWhitespaceHeight),
                    PointFont = new Font(Font.FontFamily, rightProductInfoValueFontSize),
                    Text = productPrice
                };
                var lblProductInfoArticleText = new PointLayoutItem()
                {
                    ItemID = 6,
                    ItemName = "lblProductInfoArticleText",
                    ItemType = ComponentTypes.CLabel,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(rightProductInfoBarWidth, rightProductInfoCaptionHeight),
                    PointLocation = new System.Drawing.Point(
                        (_currentScreenWidth / 2) + intProductInfoWhitespaceWidth + txtProductInfoWidth + intProductInfoWhitespaceWidth,
                        (_currentScreenHeight / 2) - ((rightProductInfoCaptionHeight + rightProductInfoValueHeight) / 2)),
                    PointFont = new Font(Font.FontFamily, rightProductInfoCaptionFontSize),
                    Text = "Article",
                    ItemObject = new PointLayoutCLabel()
                    {
                        OutlineAlignment = StringAlignment.Center,
                        OutlineLineAlignment = StringAlignment.Center,
                        OutlineWidth = rightProductInfoTextOutlineWidth
                    }
                };
                var lblProductInfoArticleValueText = new PointLayoutItem()
                {
                    ItemID = 7,
                    ItemName = "lblProductInfoArticleValueText",
                    ItemType = ComponentTypes.Label,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(rightProductInfoBarWidth, rightProductInfoValueHeight),
                    PointLocation = new System.Drawing.Point(
                        (_currentScreenWidth / 2) + intProductInfoWhitespaceWidth + txtProductInfoWidth + intProductInfoWhitespaceWidth,
                        (_currentScreenHeight / 2) - ((rightProductInfoCaptionHeight + rightProductInfoValueHeight) / 2) + rightProductInfoCaptionHeight),
                    PointFont = new Font(Font.FontFamily, rightProductInfoValueFontSize),
                    Text = productArticle
                };
                var lblProductInfoBarcodeText = new PointLayoutItem()
                {
                    ItemID = 8,
                    ItemName = "lblProductInfoBarcodeText",
                    ItemType = ComponentTypes.CLabel,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(rightProductInfoBarWidth, rightProductInfoCaptionHeight),
                    PointLocation = new System.Drawing.Point(
                        (_currentScreenWidth / 2) + intProductInfoWhitespaceWidth + txtProductInfoWidth + intProductInfoWhitespaceWidth,
                        (_currentScreenHeight / 2) - ((rightProductInfoCaptionHeight + rightProductInfoValueHeight) / 2) + (rightProductInfoCaptionHeight + rightProductInfoValueHeight) + rightProductInfoWhitespaceHeight),
                    PointFont = new Font(Font.FontFamily, rightProductInfoCaptionFontSize),
                    Text = "Barcode",
                    ItemObject = new PointLayoutCLabel()
                    {
                        OutlineAlignment = StringAlignment.Center,
                        OutlineLineAlignment = StringAlignment.Center,
                        OutlineWidth = rightProductInfoTextOutlineWidth
                    }
                };
                var lblProductInfoBarcodeValueText = new PointLayoutItem()
                {
                    ItemID = 9,
                    ItemName = "lblProductInfoBarcodeValueText",
                    ItemType = ComponentTypes.Label,
                    Theme = cmnTheme,
                    OperationID = 0,
                    PointSize = new Size(rightProductInfoBarWidth, rightProductInfoValueHeight),
                    PointLocation = new System.Drawing.Point(
                        (_currentScreenWidth / 2) + intProductInfoWhitespaceWidth + txtProductInfoWidth + intProductInfoWhitespaceWidth,
                        (_currentScreenHeight / 2) - ((rightProductInfoCaptionHeight + rightProductInfoValueHeight) / 2) + rightProductInfoCaptionHeight + (rightProductInfoCaptionHeight + rightProductInfoValueHeight) + rightProductInfoWhitespaceHeight),
                    PointFont = new Font(Font.FontFamily, rightProductInfoValueFontSize),
                    Text = productBarcode
                };

                var btnProductInfoUp = new PointLayoutItem()
                {
                    ItemID = 10,
                    ItemName = "btnProductInfoUp",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 107,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) - cmnBtnWidth - (cmnBtnWhitespaceInWidth / 2) - cmnBtnWidth - cmnBtnWhitespaceInWidth, _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize),
                    Text = "Вверх"
                };
                var btnProductInfoDown = new PointLayoutItem()
                {
                    ItemID = 11,
                    ItemName = "btnProductInfoDown",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 108,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) - cmnBtnWidth - (cmnBtnWhitespaceInWidth / 2), _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize),
                    Text = "Вниз"
                };
                var btnProductInfoShares = new PointLayoutItem()
                {
                    ItemID = 12,
                    ItemName = "btnProductInfoPromotions",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 1,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) + (cmnBtnWhitespaceInWidth / 2), _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize),
                    Text = "Акции",
                    ItemObject = new PointLayoutButton()
                    {
                        //LoadAreaID = 4
                    }
                };
                var btnProductInfoSettings = new PointLayoutItem()
                {
                    ItemID = 13,
                    ItemName = "btnProductInfoSettings",
                    ItemType = ComponentTypes.Button,
                    Theme = cmnTheme,
                    OperationID = 1,
                    PointSize = new Size(cmnBtnWidth, cmnBtnHeight),
                    PointLocation = new System.Drawing.Point((_currentScreenWidth / 2) + cmnBtnWidth + cmnBtnWhitespaceInWidth, _currentScreenHeight - cmnBtnHeight - cmnBtnWhitespaceFromBottom),
                    PointFont = new Font(Font.FontFamily, cmnBtnFontSize),
                    Text = "Настройки",
                    ItemObject = new PointLayoutButton()
                    {
                        LoadAreaID = 2
                    }
                };

                var productInfoArea = new PointLayoutArea()
                {
                    AreaID = 3,
                    AreaName = "productInfoArea",
                    AreaSize = new Size(_currentScreenWidth, _currentScreenHeight),
                    AreaLocation = new System.Drawing.Point(0, 0),
                    Items = new List<PointLayoutItem>()
                    {
                        lblProductInfoText,
                        imgProductInfoStore,
                        imgProductInfoPicture,
                        lblProductInfoDescriptionText,
                        lblProductInfoPriceText,
                        lblProductInfoPriceValueText,
                        lblProductInfoArticleText,
                        lblProductInfoArticleValueText,
                        lblProductInfoBarcodeText,
                        lblProductInfoBarcodeValueText,
                        btnProductInfoUp,
                        btnProductInfoDown,
                        btnProductInfoShares,
                        btnProductInfoSettings
                    }
                };

                #endregion

                _layout = new PointLayoutData()
                {
                    LayoutID = 0,
                    LayoutName = "ScanScreen",
                    Areas = new List<PointLayoutArea>()
                    {
                        scanArea,
                        sharesArea,
                        settingsArea,
                        productInfoArea
                    }
                };

                using (IFileManager fileManager = new FileManager())
                {

                    fileManager.WriteData(_layout, filePath);
                }
            }

            _layoutManager = new LayoutManager(this.Controls, filePath);
        }
    }
}