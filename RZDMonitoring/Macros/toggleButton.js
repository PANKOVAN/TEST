({
    ///////////////////////////////////////////////////////////////////////////
    // $iconButton        - кнопка из одной иконки (для toolbar) 
    //  name              - команда
    //  objId             - объект (datastore) который выполняет команду
    //  type="icon"       - тип                   
    //  label             - метка (название)
    //  tooltip           - подсказка      
    //  icon              - иконка          
    ///////////////////////////////////////////////////////////////////////////
    $toggleButton: function (p) {
        let command = p.name;
        if (!command) command = { command: command, label: command };
        return {
            view: "toggle",
            width: 30,
            type: m.sys.def(p.type, command.type, 'icon'),
            tooltip: m.sys.def(p.tooltip, command.tooltip, p.label, command.label),
            icon: m.sys.def(p.icon, command.icon),
            onIcon: m.sys.def(p.icon, command.icon),
            offIcon: m.sys.def(p.icon, command.icon),
            command: command,
            click: m.commands.commandClick,
        }
    }
});
