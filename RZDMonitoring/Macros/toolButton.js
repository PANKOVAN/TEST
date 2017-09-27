({
    ///////////////////////////////////////////////////////////////////////////
    // toolbutton
    // $toolbutton
    //  name              - команда
    //  type="icon"       - тип                   
    //  label             - метка (название)
    //  tooltip           - подсказка      
    //  icon              - иконка  
    //  params            - параметры 
    ///////////////////////////////////////////////////////////////////////////
    $toolButton: function (p) {
        let command = p.name;
        let type = p.type;
        let label = p.label;
        let tooltip = p.toolip;
        let icon = p.icon;
        if (!type) type = "icon";
        if (!icon) icon = command.icon;
        if (!label) label = command.label;
        if (!tooltip) tooltip = command.tooltip;
        return {
            view: "button",
            type: type,
            label: label,
            tooltip: tooltip,
            icon: icon,
            width: 32,
            click: function () { m.commands.runCommand(p.name, p.params) }
        }
    }
});
