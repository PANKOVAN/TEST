/////////////////////////////////////////////////////////////////
// Стартовая страница
// - проекты и решения
// - избранное
// - новости
// - последнее
//////////////////////////////////////////////////////////////////
({
    commands: {
        pin: {
            can: true,
            exec: function (p) {
                webix.ajax().sync().get('data/main/pin?id=' + p.id);
                let l0 = $$('L0%id%');
                let url0 = l0.data.url;
                l0.clearAll();
                l0.load(url0);
                let l2 = $$('L2%id%');
                let url2 = l2.data.url;
                l2.clearAll();
                l2.load(url2);
            }
        }
    },
    rows: [
        {
            cols: [
                {
                    rows: [
                        {
                            $titleList: {
                                id: 'L0%id%',
                                url: 'data/main/favourites',
                                title: 'Избранное',
                                onClick: { pin: function (e, id) { m.commands.execCommand('pin', { id: id, self: this, commandData: $$('L0%id%') }) } },
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
                                url: 'data/main/last',
                                title: 'Последние',
                                template: '...',
                            }
                        }
                    ]

                },
                {
                    $titleList: {
                        id: 'L2%id%',
                        url: 'data/main/prj',
                        title: 'Проекты и решения',
                        onClick: { pin: function (e, id) { m.commands.execCommand('pin', { id: id, self: this }) } },
                        template: '<div class="item"><table width="100%" class="table-condensed"><tr>\
                                    <td width="80px"><img width=80px height=60px  src="#prjImg()#" /></td>\
                                    <td width="90%"><div><span class="item-code">#code#</span><span class="item-name">#name#</span><div><div class="item-description">#description#</div></td>\
                                    <td width="40px"><div class="pin"><span class="item-ispin-#isPin# webix_icon_btn fa-map-pin"></span></div></td>\
                                  </tr></table></div>',
                    }
                },
                {
                    $titleList: {
                        id: 'L3%id%',
                        url: 'data/main/news',
                        title: 'Новости',
                        template: '...',
                    }
                },
            ]
        }
    ]
})