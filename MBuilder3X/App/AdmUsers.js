({
    onLoadJson: function (p) { $$('L1%id%').attachEvent('onAfterSelect', function (id) { m.forms.setFormFromDataObj($$('F1%id%'), $$('L1%id%')); }); },

    commands: {
        commandData: 'D1%id%',
        clsEdit: {
            can: true,
            exec: 'runDialog',
            clsType: 'Usr'
        },
        refresh: {
        },
        save: {
        },
        add: {
            newItem: {
                _type: 'MEntity', name: 'новый пользователь', entityTypeId: 'USR', ClsId: {type: 'refid', ref: 'L1%id%'} } 
        },
        delete: {
            confirm: 'Удалить пользователя(ей)...'
        },
        join: {
            confirm: 'Объединить пользователя(ей)...'
        },
        addImage: {
            title: 'Фото пользователя',
            accept: 'image/png, image/gif, image/jpeg'
        },
        deleteImage: {
            can: m.commands.testSelected,
            exec: 'run',
            confirm: 'Удалить фотографию(и) ...',
            urlParams: { deleteImage: 1 }
        },
    },

    rows: [
        {
            $topBar: { commands: ['$refresh', '$save', '$clsEdit', '$add', '$delete', '$join', '$addImage', '$deleteImage', '$clearPassword'] }
        },
        {
            cols: [
                // Классификатор с поиском
                {
                    rows: [
                        {
                            $titleTree: {
                                id: 'L1%id%',
                                css: 'section-filter',
                                title: 'Классы (группы) пользователей',
                                url: 'data/main/cls?clstype=usr',
                                template: '{common.icon()} {common.folder()} [#code#] #name#'
                            }
                        },
                        {
                            $titleForm: {
                                id: 'F2%id%',
                                css: 'section-filter',
                                title: 'Отбор',
                                elements:
                                    [
                                        {
                                            cols: [
                                                { 'name': 'code', label: 'Логин', view: 'text', gravity: 1, labelPosition: 'top' },
                                                { 'name': 'name', label: 'ФИО', view: 'text', gravity: 5, labelPosition: 'top' }
                                            ]
                                        },
                                        {
                                            cols: [
                                                { name: 'isAll', view: 'checkbox', labelRight: 'все пользователи', labelWidth: 0 },
                                                { name: 'isDeleted', view: 'checkbox', labelRight: 'уволенные', labelWidth: 0 },
                                            ]
                                        },
                                        { $bottomBar: { commands: ['$refresh'] } }
                                    ],

                            }

                        }
                    ]
                },
                {
                    rows: [
                        {
                            $titleDataTable: {
                                id: 'D1%id%',
                                css: 'section-data',
                                title: 'Пользователи',
                                url: 'data/main/usr?clsid=#master.id#&code=#code#&name=#name#&isAll=#isAll#&isDeleteded=#isDeleted#',
                                save: 'data/main/usr',
                                masterData: 'L1%id%',
                                masterForm: 'F2%id%',
                                className: 'MEntity',
                                columns: [
                                    { id: 'code', header: 'Логин' },
                                    { id: 'name', header: 'ФИО', fillspace: true },
                                ]
                            }
                        }
                    ]
                },
                {
                    view: 'tabview',
                    cells: [
                        {
                            header: 'Свойства',
                            body: {
                                $titleForm: {
                                    id: 'F1%id%',
                                    className: 'MEntity',
                                    elements:
                                        [
                                            { 'name': 'description', gravity: 2, view: 'textarea' },
                                            { 'name': 'rem', gravity: 2, view: 'textarea' },
                                        ],

                                },
                            }
                        },
                        {
                            header: 'Роли',
                            body: {
                                view: 'template',
                                template: '<h1>Роли</h1>'
                            }
                        }
                    ]


                },
            ]
        }
    ]
})