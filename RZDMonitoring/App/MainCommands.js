({
    commands: {
        start: { can: true },
        finder: { can: true },
        monitoring: { can: true },
        monitoringAdmin: { can: true },
        monitoringViewer: { can: true },
        editor: { can: true },
        trans: { can: true },
        transViewer: { can: true },
        transDic: { can: true },
        transTools: { can: true },
        refs: { can: true },
        refsLang: { can: true },
        refsRubr: { can: true },
        admin: { can: true },
        adminUsers: { can: true },
    },
    $mainBar: {
        commands: [
            { $mainButton: { command: 'start' } },
            { $mainButton: { command: 'finder' } },
            {
                $mainButton: {
                    command: 'monitoring',
                    submenu: [
                        { command: 'monitoringAdmin' },
                        { command: 'monitoringViewer' },
                    ]
                }
            },
            { $mainButton: { command: 'editor' } },
            {
                $mainButton: {
                    command: 'trans',
                    submenu: [
                        { command: 'transViewer' },
                        { command: 'transDic' },
                        { command: 'transTools' },
                    ]
                }
            },
            {
                $mainButton: {
                    command: 'refs',
                    submenu: [
                        { command: 'refsLang' },
                        { command: 'refsRubr' },
                    ]
                }
            },
            {
                $mainButton: {
                    command: 'admin',
                    submenu: [
                        { command: 'adminUsers' },
                    ]
                }
            },
        ]
    }
})
