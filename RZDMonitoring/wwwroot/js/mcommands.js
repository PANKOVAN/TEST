if (typeof (m) == 'undefined') m = {};
///////////////////////////////////////////////////////////////////////////
// Команды
// 
// 
//
//
///////////////////////////////////////////////////////////////////////////
m.commands = {
    // Подготовить систему команд для окна (установить ссылки в элементах на описатели команд)
    prepareBind: function (root) {
        if (typeof (root) == 'string') root = $$(root);
        if (root) {
            root.sourceCommandList = [];    // полный список источников команд
            root.bindingCommandList = [];   // полный cписок описателей команд
            root.bindingCommandStack = [];  // стек описателей команд
            this._prepareBind(root, root);
            this.bind(root);
        }
    },
    _prepareBind: function (cur, root) {
        if (cur) {
            if (cur.config) {
                // Встречен описатель команд
                if (cur.config.commands) {
                    root.bindingCommandList.push(cur);
                    root.bindingCommandStack.push(cur);
                    // установить ссылку на источник данных для опистеля команд
                    if (typeof (cur.config.commands.commandData) == 'string') cur.config.commands.commandData = $$(cur.config.commands.commandData);
                }
                // Встречен элемент с командой например кнопка
                if (cur.config.command) {
                    cur.config.command = this.findCommand(cur.config.command);
                    if (cur.config.command) {
                        root.sourceCommandList.push(cur);
                        // установить ссылку на источник данных 
                        if (typeof (cur.config.commandData) == 'string') cur.config.commandData = $$(cur.config.commandData);
                        // для кнопки поставить ссылку на ее commandBind
                        let disable = true;
                        let name = cur.config.command.command;
                        for (let i = root.bindingCommandStack.length - 1; i >= 0; i--) {
                            let commands = root.bindingCommandStack[i].config.commands;
                            if (commands[name]) {
                                cur.config.commandBind = commands[name];
                                if (!cur.config.commandData) cur.config.commandData = commands.commandData;
                                disable = false;
                                break;
                            }
                        }
                        if (disable) {
                            // Есть стандартная команда
                            if (this[name]) {
                                //if (!cur.config.commandData) cur.config.commandData = commands.commandData;
                            }
                            // Описателя нет
                            else {
                                cur.disable();
                            }
                        }
                    }
                }
                // Встречено меню
                if (cur.config.view == 'menu') {
                    // установить ссылку на источник данных
                    if (typeof (cur.config.commandData) == 'string') cur.config.commandData = $$(cur.config.commandData);
                    // для меню нужны все описатели команд
                    root.sourceCommandList.push(cur);
                    this._prepareBindMenu(cur.config.data, cur, root);
                }
                // Для bind
                cur._bindroot = root;
            }

            // для источников данных присоединяем обработку событий с bind
            this._bindAttach(cur);

            // обходим детей
            let childs = cur.getChildViews();
            for (let i in childs) {
                this._prepareBind(childs[i], root);
            }
            // popup это не дети поэтому их обходим отдельно
            if (cur.config.popup) {
                let popup = $$(cur.config.popup);
                popup.bindRoot = root;
                this._prepareBind(popup, root);
            }

            if (cur.config) {
                if (cur.config.commands) {
                    root.bindingCommandStack.pop();
                }
            }
        }

    },
    _prepareBindMenu: function (data, menu, root) {
        if (data && root) {
            for (let i = 0; i < data.length; i++) {
                let item = data[i];
                let disable = true;
                if (item.data) {
                    this._prepareBindMenu(item.data, menu, root);
                }
                else if (item.command) {
                    item.command = this.findCommand(item.command);
                    if (item.command) {
                        let name = item.command.command;
                        let commandBind = null;
                        for (let j = root.bindingCommandStack.length - 1; j >= 0; j--) {
                            let controlBind = root.bindingCommandStack[j];
                            if (controlBind.config && controlBind.config.commands) {
                                commandBind = controlBind.config.commands[name];
                                if (commandBind) {
                                    item.commandBind = commandBind;
                                    if (!item.commandData) item.commandData = controlBind.config.commands.commandData;
                                    disable = false;
                                    break;
                                }
                            }
                        }
                        if (disable) {
                            // Есть стандартная команда
                            if (this[name]) {
                                //if (!item.commandData) item.commandData = controlBind.config.commands.commandData;
                            }
                            // Описателя нет
                            else {
                                menu.disable(item.id);
                            }
                        }
                    }
                }
            }
        }
    },
    // Подготовить параметры
    _prepareParms: function (commandBind, commandData, commandParm, command, self) {
        let parms = {};
        for (let i in commandBind) {
            if (i != 'can' && i != 'exec') parms[i] = commandBind[i];
        }
        parms.commandData = commandData;
        parms.commandParm = commandParm;
        parms.command = command;
        parms.self = self;
        return parms;
    },
    // Выполнить команду по ее названию
    execCommand: function (command, p) {
        let commandName = command.command || command;
        if (command) {
            if (p) {
                let control = p.self;
                while (control) {
                    let commands = control.config.commands;
                    if (commands) {
                        let command = commands[commandName];
                        if (command) {
                            if (!p) p = {};
                            for (let n in command) {
                                if (n != 'can' && n != 'exec') {
                                    p[n] = command[n];
                                }
                            }
                            let can = command.can;
                            if (typeof (can) == 'function') can = can(p);
                            else if (typeof (can) == 'string') {
                                if (m.commands[can]) can = m.commmands[can](p);
                                else can = false;
                            }
                            if (can) {
                                let exec = command.exec;
                                if (typeof (exec) == 'function') exec(p);
                                else if (typeof (exec) == 'string') {
                                    if (m.commands[exec]) m.commands[exec](p);
                                }
                            }
                            return;
                        }
                    }
                    control = control.getParentView();
                }
            }
            if (typeof (this[commandName]) == 'function') this[commandName](p);
        }
    },
    // Выполнить команду по ее описателю
    execBind: function (commandBind, commandData, command, self) {
        let parms = this._prepareParms(commandBind, commandData, null, command, self);
        command = this.findCommand(command);
        if (commandBind) {
            let can = commandBind.can;
            if (typeof (can) == 'function') can = can.call(this, parms);
            if (can) {
                if (typeof (commandBind.exec) == 'function') {
                    commandBind.exec.call(this, parms);
                }
                else if (typeof (commandBind.exec) == 'string') {
                    if (typeof (this[commandBind.exec]) == 'function') {
                        this[commandBind.exec].call(this, parms);
                    }
                }
                else if (typeof (this[command.command]) == 'function') {
                    this[command.command].call(this, parms);
                }
                else {
                    m.main.runCommand(parms.command.command, parms, true);
                }
            }
        }
        else {
            if (typeof (this[command.command]) == 'function') {
                this[command.command](parms);
            }
        }
        //this.bind(self);
    },
    commandClick: function (objid, self) {
        let o = $$(objid);
        if (o && o.config) m.commands.execBind(o.config.commandBind, o.config.commandData, o.config.command, this);
    },
    menuClick: function (id, self) {
        if (self) {
            let item = self.getMenuItem(id);
            this.execBind(item.commandBind, item.commandData, item.command, self)
            self.hide();
        }
    },
    findCommand: function (command) {
        if (typeof (command) == 'string') {
            let command1 = m.macro.getCommand(command);
            if (command1) return command1;
            m.main.error('Команда ' + command + 'не найдена...');
            return null;
        }
        return command;
    },
    // Пересчитать disable для элементов
    bind: function (control) {
        if (control) {
            if (control._bindroot) {
                this._bind(control._bindroot)
            }
            else if (control._settings && control._settings.master && control._settings.master._bindroot) {
                this._bind(control._settings.master._bindroot)
            }
            else {
                m.main.error("_bindroot для " + control.name + "не найден...");
            }
        }
    },
    _bind: function (bindRoot) {
        let sourceCommandList = bindRoot.sourceCommandList;
        for (let i in sourceCommandList) {
            let source = sourceCommandList[i];
            if (source.config) {
                if (source.config.commandBind) {
                    let parms = this._prepareParms(source.config.commandBind, source.config.commandData, null, source.config.command);
                    let can = source.config.commandBind.can;
                    if (typeof (can) == 'function') can = can.call(this, parms);
                    else if (typeof (can) == 'undefined') {
                        let name = source.config.command.command;
                        if (name) {
                            name = 'can' + name.substring(0, 1).toUpperCase() + name.substring(1);
                            can = this[name];
                            if (typeof (can) == 'function') {
                                source.config.commandBind.can = can;
                                can = can.call(this, parms);
                            }
                        }
                    }
                    if (can) source.enable();
                    else source.disable();
                }
                else if (source.config.view == 'menu') {
                    this._bindMenu(source.config.data, source);
                }
                //else {
                //    source.disable();
                //}
            }
        }
    },
    _bindMenu: function (data, menu) {
        for (let i = 0; i < data.length; i++) {
            let item = data[i];
            if (item.data) {
                this._bindMenu(item.data, menu);
            }
            else if (item.command && item.commandBind) {
                let parms = this._prepareParms(item.commandBind, item.commandData, item.command);
                let can = item.commandBind.can;
                if (typeof (can) == 'function') can = can.call(this, parms);
                else if (typeof (can) == 'undefined') {
                    let name = item.command.command;
                    if (name) {
                        name = 'can' + name.substring(0, 1).toUpperCase() + name.substring(1);
                        can = this[name];
                        if (typeof (can) == 'function') {
                            item.commandBind.can = can;
                            can = can.call(this, parms);
                        }
                    }
                }
                if (can) menu.enableItem(item.id);
                else menu.disableItem(item.id);
            }
        }
    },
    _bindAttach: function (control) {
        // События порождающие привязку команд к текущему состоянию
        if (control.select) {
            control.attachEvent('onBeforeSelect', function () { let dp = webix.dp(this); dp.send(); })
            control.attachEvent('onAfterSelect', function () { m.commands.bind(this); })
            let dp = webix.dp(control);
            if (dp) {
                dp.attachEvent('onAfterSave', function () { m.commands.bind(this); })
            }
        }
        else if (control.view == 'form') {
            control.attachEvent('onChange', function () { m.commands.bind(this) })
        }

        // Связываем источники данных по masterData
        let masterData = control.config.masterData;
        if (masterData) {
            if (typeof (masterData) == 'string') masterData = $$(masterData);
            if (masterData) {
                control.bind(masterData);
                if (control.config.view == 'form') {
                    control.attachEvent('onChange', function () { m.forms.setDataObjFromForm(control, masterData) })
                }
                if (masterData.select && control.config.url) {
                    control.config._url = control.config.url;

                    masterData.attachEvent('onAfterSelect', function (id) {
                        m.commands.refreshData(control, id)
                    })
                }
            }
            else {
                m.main.error('Параметр конфигурации "masterData" задан неправильно.');
            }
        }

    },
    ///////////////////////////////////////////////////////////////
    // стандартные команды
    ///////////////////////////////////////////////////////////////
    testUpdates: function (p) {
        let data = p.commandData;
        if (data) {
            let dp = webix.dp(data);
            if (dp) return dp._updates && dp._updates.length > 0;
        }
        return false;
    },
    testSelected: function (p) {
        let data = p.commandData;
        if (data && data.getSelectedId) {
            return m.getSelectedCount(data) > 0;
        }
        return false;
    },
    testOneSelected: function (p) {
        let data = p.commandData;
        if (data && data.getSelectedId) {
            return m.getSelectedCount(data) == 1;
        }
        return false;
    },
    testMultiSelected: function (p) {
        let data = p.commandData;
        if (data && data.getSelectedId) {
            return m.getSelectedCount(data) > 1;
        }
        return false;
    },
    ///////////////////////////////////////////////////////////////
    // Системные команды
    ///////////////////////////////////////////////////////////////

    json: function (p) {
        m.commands.showJson(p.self);
    },
    close: function (p) {
        m.main.close(p.self);
    },
    help: function () {
        alert('HELP');
    },
    ///////////////////////////////////////////////////////////////
    // стандартные команды
    ///////////////////////////////////////////////////////////////
    canRunDialog: function (p) {
        return true;
    },
    runDialog: function (p) {
        m.main.runDialog(p.command.command, p);
    },
    ////////////////////////////////////////////

    // Перечитатать данные с подстановкой параметров 
    refreshData: function (data, masterId) {
        // Запомним текущий ID
        let selectedId = '';
        let sel = data.getSelectedId(true, true);
        if (sel.length == 1) selectedId = sel[0];
        // Всякий раз после загрузки url портится за счет подстановки текущихпараметров поэтому нужно сохранить начальное значение url в _url
        if (!data.config._url) data.config._url = data.config.url;
        // Если url задан то есть о чем говорить
        if (data.config._url) {
            data.clearAll();
            let url = data.config._url;
            // Подстановка masterId
            if (typeof (masterId) != 'undefined') {
                url = url.replace('#master.id#', masterId);
            }
            else if (data.config.masterData) {
                if (typeof (data.config.masterData) == 'string') data.config.masterData = $$(data.config.masterData);
                if (data.config.masterData) {
                    masterId = m.getSelectedId(data.config.masterData);
                    if (typeof (masterId) != 'undefined') {
                        url = url.replace('#master.id#', masterId);
                    }
                }
            }
            url = url.replace('#master.id#', '');
            url = url.replace('#selected.id#', selectedId);

            // Подстановка параметров 
            if (data.config.masterForm) {
                if (typeof (data.config.masterForm) == 'string') data.config.masterForm = $$(data.config.masterForm);
                let values = data.config.masterForm.getValues();
                let matches = url.match(/#\w+#/igm);
                for (let i in matches) {
                    let m = matches[i];
                    let n = m.substring(1, m.length - 1);
                    let v = '';
                    if (typeof (values[n]) != 'undefined') v = values[n];
                    url = url.replace(m, v);
                }
            }

            data.load(url);
        }
    },

    canRefresh: function (p) {
        return true;
    },
    refresh: function (p) {
        let data = p.commandData;
        if (data && data.data) {
            this.refreshData(data);
            //let url = data.data.url;
            //data.clearAll();
            //data.load(url);
        }
    },
    canSave: function (p) {
        return this.testUpdates(p);
    },
    //////////////////////////////////////
    save: function (p) {
        let data = p.commandData;
        if (data) {
            let dp = webix.dp(data);
            if (dp) dp.send();
        }
    },
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Добавить новый объект в dataobj
    /// dataobj - источник (хранилище) данных, список, список, таблица и т.д.
    /// newobj - новый объект, который будет добавлен, должен содержать поля:
    ///        _type: '<тип(класс) объекта>'
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    canAdd: function (p) {
        return true;
    },
    add: function (p) {
        let data = p.commandData;
        if (data && p.newItem) {
            let newItem = m.sys.copy({}, p.newItem);
            newItem.id = -1;
            newItem = m.data.setProto(newItem);
            let curPos = null;
            let curId = m.getSelectedId(data);
            let curObj = data.getSelectedItem();
            if (curId) curPos = data.getIndexById(curId);
            if (curPos) curPos++;
            let id = null;

            if (newItem.parentId) id = data.add(newItem, curPos, newItem.parentId);
            else id = data.add(newItem, curPos);

            data.select(id);
            data.showItem(id);

            let dp = webix.dp(data);
            if (dp) dp.send();
        }
    },
    canAddChild: function (p) {
        return this.testOneSelected(p);
    },
    addChild: function (p) {
        this.add(p);
    },
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Удалить текущую строку из источника данных (или несколько выбранных)
    /// - на клиенте текущая (или несколько строк) строка удаляется, 
    /// - на сервере генерится стандартная последовательность на удаление соответсвующая этому типу данных
    /// dataobj - источник (хранилище) данных, список, список, таблица и т.д.
    /// params  - параметры
    /// confirm - строка предупрежения
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    canDelete: function (p) {
        return this.testSelected(p);
    },
    delete: function (p) {
        let data = p.commandData;
        if (data && this.testSelected(p)) {
            let confirm = p.confirm || "Удалить...";
            webix.confirm(
                {
                    text: confirm,
                    callback: function (result) {
                        if (result) {
                            let id = m.getSelectedId(data);
                            let dp = webix.dp(data);
                            if (typeof (id) == 'object') {
                                for (var i in id) {
                                    data.remove(id[i]);
                                    if (dp) dp.send();
                                }
                            }
                            else {
                                data.remove(id);
                                if (dp) dp.send();
                            }
                        }
                    }
                });
            {
            }
        }
    },
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Удалить текущую строку из источника данных (или несколько выбранных)
    /// - на клиенте текущая (или несколько строк) строка удаляется, 
    /// - на сервере генерится стандартная последовательность на удаление соответсвующая этому типу данных
    /// dataobj - источник (хранилище) данных, список, список, таблица и т.д.
    /// params  - параметры
    /// confirm - строка предупрежения
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    canJoin: function (p) {
        return this.testMultiSelected(p);
    },
    join: function (p) {
        let data = p.commandData;
        if (data && this.testMultiSelected(p)) {
            let confirm = p.confirm || "Объединить выделенные строки...";
            webix.confirm(
                {
                    text: confirm,
                    callback: function (result) {
                        if (result) {
                            let url = m.sys.def(p.url, data.data.url)
                            let id = m.getSelectedId(data);
                            if (typeof (id) == 'object') {
                                let s = '';
                                for (var i in id) {
                                    s += ',' + id[i];
                                }
                                id = s.substring(1);
                            }
                            url = m.sys.urlAdd(url, 'id', id)
                            url = m.sys.urlAdd(url, 'join', 1)
                            let url1 = data.url;
                            try {
                                data.load(url);
                            }
                            finally {
                                data.url = url1;
                            }
                        }
                    }
                });
        }
    },
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Удалить текущую строку из источника данных (или несколько выбранных)
    /// - на клиенте текущая (или несколько строк) строка удаляется, 
    /// - на сервере генерится стандартная последовательность на удаление соответсвующая этому типу данных
    /// dataobj - источник (хранилище) данных, список, список, таблица и т.д.
    /// params  - параметры
    /// confirm - строка предупрежения
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    canAddImage: function (p) {
        return this.testSelected(p);
    },
    addImage: function (p) {
        let data = p.commandData;
        if (data && this.testSelected(p)) {
            m.main.runDialog('sysUpload', p)
        }
    },
    // Выполнить команду для докумена(строки) из источника данных (или несколько выбранных)
    run: function (p) {
        let data = p.commandData;
        if (data) {
            // если задан masterId то нужно установить текущую строку, masterId бывает задан если источником команды является активное содержимое (activeContent)
            /*
            if (params.$masterId && dataobj) {
                dataobj.select(params.$masterId);
            }
            */
            // url как правило сидит в мсточнике данных но может задаваться и явно
            let url = m.sys.def(p.url, data.data.url)
            // сформируем параметр id, который может быть и списком id
            //let id = params.$masterId;
            //if (!id) {
            let id = m.getSelectedId(data);
            if (typeof (id) == 'object') {
                let s = '';
                for (var i in id) {
                    s += ',' + id[i];
                }
                id = s.substring(1);
            }
            //}
            // добавим к url стаедартные параметры (mode и id) и все что есть в params
            url = m.sys.urlAdd(url, 'id', id)
            url = m.sys.urlAddParams(url, p.urlParams);
            // нужно еще запомнить старый url источника чтобы он не менялся
            // если задан запрос на подтвержение команды, то вызываем диалог на подтвеждение
            if (p.confirm) {
                webix.confirm(
                    {
                        text: p.confirm,
                        callback: function (result) {
                            if (result) {
                                let url1 = data.url;
                                try {
                                    data.load(url);
                                }
                                finally {
                                    data.url = url1;
                                }
                            }
                        }
                    }
                );
            }
            else {
                let url1 = data.url;
                try {
                    data.load(url);
                }
                finally {
                    data.url = url1;
                }
            }
        }
    },
    // Показать настройки окна
    _parseJson: function (name, json, tree) {
        if (!tree) tree = [];
        let node = {};
        node.name = name;
        node.json = json;
        node.webix_kids = (typeof (json) == 'object');
        tree.push(node);
        return tree;
    },
    _openJson: function (self, id) {
        let item = self.getItem(id);
        if (item && typeof (item.json) == 'object') {
            if (!item.data) {
                let data = [];
                for (let n in item.json) {
                    m.commands._parseJson(n, item.json[n], data);
                }
                self.parse({parent:id, data:data });
            }
        }
    },
    _templateJson: function (obj, com) {
        let type = typeof (obj.json);
        let value = '';
        switch (type) {
            case 'object':
                if (typeof (obj.json.length) == 'undefined') value = '{...}';
                else value = '[' + obj.json.length+']';
                break;
            case 'function':
                value = 'function() {...}';
                break;
            default:
                value = obj.json;
                break;
        }
        return com.icon(obj, com) + '<span>' + obj.name + '</span> <span> : </span> <span>' + value + '</span>';
    },
    showJson: function (controlobj) {
        if (typeof (controlobj == 'string')) controlobj = $$(controlobj);
        while (controlobj) {
            if (controlobj._json_) {
                m.main.runDialog('admJson', { json: this._parseJson('<>', controlobj._json_) });
                break;
            }
            controlobj = controlobj.getParentView();
        }
    },
    showTypes: function () {
        m.main.runDialog('admJson', { json: m.macro._modelClasses, command: 'showTypes' });
    },
    showBaseTypes: function () {
        m.main.runDialog('admJson', { json: m.macro._baseClasses, command: 'showBaseTypes' });
    },
};