({
    commands: {
        start: { can: true },
        adm: { can: true },
        prjEdit: { can: true },
        clsEdit: { can: true },
        admUsers: { can: true },
        admRoles: { can: true },
        showTypes: { can: true },
        showBaseTypes: { can: true },
    },
    $mainBar: {
        commands: [
            { $mainButton: { command: 'start' } },
            {
                $mainButton: {
                    command: 'adm',
                    submenu: [
                        { command: 'prjEdit' },
                        { command: 'clsEdit' },
                        { command: 'admUsers' },
                        { command: 'admRoles' },
                        { separator: true },
                        { command: 'showTypes' },
                        { command: 'showBaseTypes' },
                    ]
                }
            },
        ]
    }
})
