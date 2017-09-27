/////////////////////////////////////////////////////////////////
// Стартовая страница
// - проекты и решения
// - избранное
// - новости2222222222222
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
            newItem: { _type: 'MLang', header: 'новый язык' }
        },
        delete: {
            confirm: 'Удалить язык(и)...'
        },
        join: {
            confirm: 'Объединить язык(и)...'
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
                        url: 'data/main/refslang',
                        save: 'data/main/refslang',
                        className: 'MLang',
                        columns: [
                            { id: 'code', },
                            { id: 'header', fillspace: true  },
                        ]
                    }
                },
                {}
            ]
        }
    ]
})