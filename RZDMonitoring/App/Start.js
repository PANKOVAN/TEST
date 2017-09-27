/////////////////////////////////////////////////////////////////
// Стартовая страница
// - проекты и решения
// - избранное
// - новости1111111111111111111111111111
// - последнее
//////////////////////////////////////////////////////////////////
({
    commands: {
    },
    rows: [
        {
            cols: [
                {
                    $titleList: {
                        id: 'L0%id%',
                        url: 'data/main/favourites',
                        title: 'Избранное',
                        template: '<div class="item"><table width="100%" class="table-condensed"><tr>\
                                    <td width="80px"><img width=80px height=60px  src="#prjImg()#" /></td>\
                                    <td width="90%"><div><span class="item-code">#code#</span><span class="item-name">#name#</span><div><div class="item-description">#description#</div></td>\
                                    <td width="40px"><div class="pin"><span class="item-ispin-#isPin# webix_icon_btn fa-map-pin"></span></div></td>\
                                  </tr></table></div>',
                    }
                },
                {
                    $titleList: {
                        id: 'L1%id%',
                        url: 'data/main/news',
                        title: 'Новости',
                        template: '<div class="item"><table width="100%" class="table-condensed"><tr>\
                                    <td width="80px"><img width=80px height=60px  src="#prjImg()#" /></td>\
                                    <td width="90%"><div><span class="item-code">#code#</span><span class="item-name">#name#</span><div><div class="item-description">#description#</div></td>\
                                    <td width="40px"><div class="pin"><span class="item-ispin-#isPin# webix_icon_btn fa-map-pin"></span></div></td>\
                                  </tr></table></div>',
                    }
                },
            ]
        }
    ]
})