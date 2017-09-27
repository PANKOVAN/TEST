({
    ///////////////////////////////////////////////////////////////////////////
    //  $mainButton - кнопка главного меню
    //  command           - команда
    //  label             - метка (название)
    //  tooltip           - подсказка      
    //  params            - параметры 
    ///////////////////////////////////////////////////////////////////////////
    $mainButton: function (p) {
        //m.sys.bp();
        if (p.submenu) {
            let popup = {};
            popup.view = 'popup';
            popup.id = p.command + '-submenu';
            popup.width = 300;
            popup.body = {};
            popup.body.view = 'menu';
            popup.body.css = 'main-popup-menu';
            popup.body.layout = 'y';
            popup.body.autoheight = true;
            popup.body.select = true;
            popup.body.borderless = true;
            popup.body.template = function (o) {
                return '<img src="'+o.image+'" style="width:24px; margin-right:4px;"/><span>' + o.value+'</span>';
            };
            popup.body.data = [];
            popup.body.click = function (id, e, t) { m.commands.menuClick(id, this); };
            popup.on = { onBeforeShow: function () { m.commands.bind(this) } };
            for (let i in p.submenu) {
                let item = p.submenu[i];
                if (item.command) {
                    let command = m.commands.findCommand(item.command);
                    if (!command) command = {};
                    let item1 = {
                        id: m.sys.def(command.command, item.command),
                        value: m.sys.def(item.label, command.label),
                        image: 'images/menu/icons8-' + m.sys.def(item.img, command.icon) + '.png',
                        //icon: m.sys.def(item.icon, command.icon),
                        command: m.sys.def(command.command, item.command),
                    }
                    popup.body.data.push(item1);
                }
                else if (item.separator) {
                    popup.body.data.push({ $template: 'Separator' });
                }
            }
            webix.ui(popup);
            let command = m.commands.findCommand(p.command);
            if (!command) command = {};
            return {
                id: '_' + m.main.butId(m.sys.def(command.command, p.command)),
                command: m.sys.def(command.command, p.command),
                view: 'button',
                type: 'image',
                image: 'images/main/icons8-' + m.sys.def(p.img, command.icon) + '.png',
                //type: 'icon',
                //icon: m.sys.def(p.icon, command.icon),
                tooltip: m.sys.def(p.tooltip, command.tooltip, command.label),
                css: 'main-but',
                width: 48,
                align: 'left',
                popup: p.command + '-submenu'
            }
        }
        let command = m.commands.findCommand(p.command);
        if (!command) command = {};
        return {
            id: '_' + m.main.butId(m.sys.def(command.command, p.command)),
            command: m.sys.def(command.command, p.command),
            view: 'button',
            type: 'image',
            image: 'images/main/icons8-' + m.sys.def(p.img, command.icon)+'.png',
            //type: 'icon',
            //icon: m.sys.def(p.icon, command.icon),
            tooltip: m.sys.def(p.tooltip, command.tooltip, command.label),
            css: 'main-but',
            width: 48,
            align: 'left',
            click: m.commands.commandClick,
            //click: function () { m.main.runCommand(p.command, p.params) },
        }
    }
});
