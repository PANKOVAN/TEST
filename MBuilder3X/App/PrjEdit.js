({

    commands: {
        commandData: 'L%id%',
        clsEdit: {
            can: true,
            exec: 'runDialog',
            clsType: 'Prj'
        },
        refresh: {
        },
        save: {
        },
        add: {
            newItem: { _type: 'MEntity', name: 'новый проект', entityTypeId: 'PRJ' }
        },
        delete: {
            confirm: 'Удалить проект(ы)...'
        },
        join: {
            confirm: 'Объединить проект(ы)...'
        },
        addImage: {
            title: 'Пиктограмма проекта',
            accept: 'image/png, image/gif, image/jpeg'
        },
        deleteImage: {
            can: m.commands.testSelected,
            exec: 'run',
            confirm: 'Удалить пиктограмму(ы) ...',
            urlParams: { deleteImage: 1 }
        },
    },
    rows: [
        {
            $topBar: { commands: ['$refresh', '$save', '$clsEdit', '$add', '$delete', '$join', '$addImage', '$deleteImage'] }
        },
        {
            cols: [
                {
                    $titleList: {
                        id: 'L%id%',
                        css: 'section-data',
                        url: 'data/main/prj',
                        save: 'data/main/prj',
                        title: 'Список проектов',
                        template: '<div class="item"><table width="100%" class="table-condensed"><tr>\
                                    <td width="80px"><img width=80px height=60px  src="#prjImg()#" /></td>\
                                    <td width="90%"><div><span class="item-code">#code#</span><span class="item-name">#name#</span><div><div class="item-description">#description#</div></td>\
                                  </tr></table></div>',
                    }
                },
                {
                    $titleForm: {
                        id: 'F%id%',
                        title: 'Свойства',
                        css: 'section-data',
                        masterData: 'L%id%',
                        className: 'MEntity',
                        elements:
                            [
                                {
                                    cols: [
                                        {
                                            rows: [
                                                { 'name': 'code', },
                                                { 'name': 'name' },
                                            ]
                                        },
                                        {
                                            view: 'template',
                                            borderless: true,
                                            masterData: 'L%id%',
                                            template: function (o) { if (o.prjImg) return '<img style="width:400px;" src="' + o.prjImg() + '"></img>'; else return ''; },
                                            width: 400,
                                            height:300,
                                        }
                                    ]
                                },
                                { 'name': 'description', gravity: 2, view: 'textarea' },
                                { 'name': 'rem', gravity: 2, view: 'textarea' },
                            ],

                    },
                }
            ]
        }
    ]
})