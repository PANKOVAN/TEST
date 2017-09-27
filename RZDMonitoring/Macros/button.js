({
    ///////////////////////////////////////////////////////////////////////////
    // button
    // $button
    //  name              - команда
    //  objId             - объект (datastore) который выполняет команду
    //  type="icon"       - тип                   
    //  label             - метка (название)
    //  tooltip           - подсказка      
    //  icon              - иконка          
    ///////////////////////////////////////////////////////////////////////////
    $button: function (p) {
        let command = p.name;
        if (!command) command = { command: command, label: command };
        return {
            view: "button",
            type: m.sys.def(p.type, command.type, 'imageButton'),
            label: m.sys.def(p.label, command.label),
            tooltip: m.sys.def(p.tooltip, command.tooltip, p.label, command.label),
            image: 'images/menu/icons8-' + command.icon + '.png',
            command: command,
            click: m.commands.commandClick,
        }
    }
});
