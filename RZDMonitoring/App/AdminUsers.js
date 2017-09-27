/////////////////////////////////////////////////////////////////
// Стартовая страница
// - проекты и решения
// - избранное
// - новости
// - последнее
//////////////////////////////////////////////////////////////////
({
    commands: {
        commandData: 'D1%id%',
        refresh: {
        },
        save: {
        },
        add: {
            newItem: { _type: 'MUser', fio: 'новый пользователь' }
        },
        delete: {
            confirm: 'Удалить пользователя(eй)...'
        },
        join: {
            confirm: 'Объединить пользователя(ей)...'
        },
    },
    rows: [
        {
            $topBar: { commands: ['$refresh', '$save', '$add', '$delete', '$join'] }
        },
        {
            cols: [
                {
                    $titleDataTable: {
                        id: 'D1%id%',
                        css: 'section-data',
                        title: 'Настройка доступа',
                        url: 'data/main/adminusers',
                        save: 'data/main/adminusers',
                        className: 'MUser',
                        columns: [
                            { id: 'login', },
                            { id: 'fio', fillspace: true  },
                            { id: 'isAdmin', header: { text: 'Администратор', rotate: true, height:150 }, template: '<div style="background-color:#E80149;text-align:center">{common.checkbox()}</div>', editor: 'checkbox', width: '48' },
                            { id: 'isREditor', header: { text: 'Справочники', rotate: true, height:150 },     template: '<div style="background-color:#FBE3A4;text-align:center">{common.checkbox()}</div>', editor: 'checkbox', width: '48' },
                            { id: 'isFinder', header: { text: 'Мониторинг', rotate: true, height:150 },      template: '<div style="background-color:#09818A;text-align:center">{common.checkbox()}</div>', editor: 'checkbox', width: '48' },
                            { id: 'isModerator', header: { text: 'Модератор', rotate: true, height:150 },   template: '<div style="background-color:#0E6025;text-align:center">{common.checkbox()}</div>', editor: 'checkbox', width: '48' },
                            { id: 'isEditor', header: { text: 'Правка', rotate: true, height:150 },      template: '<div style="background-color:#59A474;text-align:center">{common.checkbox()}</div>', editor: 'checkbox', width: '48' },
                            { id: 'isTranslator', header: { text: 'Перевод', rotate: true, height:150 },  template: '<div style="background-color:#B41653;text-align:center">{common.checkbox()}</div>', editor: 'checkbox', width: '48' },
                            { id: 'isViewer', header: { text: 'Просмотр', rotate: true, height:150 },      template: '<div style="background-color:#A0C2CD;text-align:center">{common.checkbox()}</div>', editor: 'checkbox', width: '48' },
                            { id: 'isDismiss', template: '{common.checkbox()}'},
                        ]
                    }
                },
                {}
            ]
        }
    ]
})