({
    commands: {
        commandData: 'L2%id%',
        refresh: {},
        save: {},
        add: {
            newItem: { _type: 'MCls', name: 'новый класс', clsTypeId: { ref: 'L1%id%' }, parentId: { ref: 'L2%id%', name:'parentId' } } 
        },
        addChild: {
            newItem: { _type: 'MCls', name: 'новый класс', clsTypeId: { ref: 'L1%id%' }, parentId: { ref: 'L2%id%' } }
        },
        delete: {}
    },



    onLoadJson: function (p) {
        let l1 = $$('L1%id%');
        let l2 = $$('L2%id%');
        l1.load('data/main/cls?clstypelist=1&clstype=' + m.sys.def(p.clsType, ''));
    },


    id: '%id%',
    rows: [
        {
            $topBar: { commands: ['$refresh', '$save', '$add', '$addChild', '$delete'] }
        },
        {
            cols: [
                {

                    $titleList: {
                        id: 'L1%id%',
                        css:'section-filter',
                        title: 'Тип классификатора',
                        template: '<img src="#getIcon()#"></img>  [<b>#id#</b>] #header#',
                    },
                    gravity: 1,
                },
                {
                    $titleTree: {
                        id: 'L2%id%',
                        css: 'section-data',
                        title: 'Классификатор',
                        url: 'data/main/cls?clstype=#master.id#',
                        save: 'data/main/cls',
                        masterData:'L1%id%',
                        template: m.sys.treeCodeNameTemplate,
                    },
                    gravity: 2,
                },
                {
                    $titleForm: {
                        id: 'F1%id%',
                        css: 'section-data',
                        title: 'Свойства',
                        masterData: 'L2%id%',
                        className: 'MCls',
                        elements:
                            [
                                { 'name': 'code', },
                                { 'name': 'name'},
                                { 'name': 'description', gravity: 2, view: 'textarea' },
                                { 'name': 'rem', gravity: 2, view: 'textarea' },
                            ],

                    },
                    gravity: 3,
                }
            ]
        }
    ]
})