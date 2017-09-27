({
    ///////////////////////////////////////////////////////////////////////////
    // $iconButton        - кнопка из одной иконки (для toolbar) 
    //  command              - команда
    //  objId             - объект (datastore) который выполняет команду
    //  type="icon"       - тип                   
    //  label             - метка (название)
    //  tooltip           - подсказка      
    //  icon              - иконка          
    ///////////////////////////////////////////////////////////////////////////
    $iconButton: function (p) {
        let command = p.command;
        if (!command) command = { command: command, label: command };
        return {
            view: "button",
            width: 36,
            height:36,
            type: m.sys.def(p.type, command.type, 'image'),
            tooltip: m.sys.def(p.tooltip, command.tooltip, p.label, command.label),
            image: 'images/menu/icons8-' + command.icon + '.png',
            css: 'tool-but',
            command: command,
            click: m.commands.commandClick,
        }
    }
});
