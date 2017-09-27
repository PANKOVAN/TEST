if (typeof (m) == 'undefined') m = {};
///////////////////////////////////////////////////////////////////////////
// Приложение
///////////////////////////////////////////////////////////////////////////
m.main = {
    // Логин
    login: function (formName) {
        webix.ajax().post(
            "account/login",
            $$(formName).getValues(),
            function (text, data) {
                let r = data.json();
                if (r.type === 0) {
                    m.main.refreshAll();
                }
                else {
                    webix.alert({
                        //title: "Custom title",
                        //ok: "Custom text",
                        type: "alert-error",
                        text: r.data
                        //callback: function () {...}
                    });
                }
            }
        );
    },
    // Расширение webix
    _isCustomWebix: false,
    customWebix: function () {
        if (!this._isCustomWebix) {
            this._isCustomWebix = true;
            // Редактор для объектов
            webix.protoUI({
                name: 'obj',
                $cssName: 'combo',
                $objValue: null,
                setValue: function (value) {
                    if (typeof (value) == 'object') {
                        this.$objValue = value;
                        value = this._pattern(value);
                        if (value._toString) value = value._toString();
                        else if (value.value) value = value.value;
                        else if (value.name) value = value.name;
                        this.getInputNode().value = value;
                    }
                },
                getValue: function () {
                    return this.$objValue;
                }
            }, webix.ui.combo);
            webix.protoUI({
                name: 'suggestObj',
                $cssName: 'suggest',
                setMasterValue: function (data, refresh) {
                    var text = data.id ? this.getItemText(data.id) : (data.text || data.value);
                    if (this._settings.master) {
                        var master = webix.$$(this._settings.master);
                        master.setValue(data);
                    } else if (this._last_input_target) {
                        this._last_input_target.value = data;
                    }

                    if (!refresh) {
                        this.hide(true);
                        if (this._last_input_target)
                            this._last_input_target.focus();
                    }
                    this.callEvent("onValueSuggest", [data, text]);
                    webix.delay(function () {
                        webix.callEvent("onEditEnd", []);
                    });
                },
                getMasterValue: function () {
                    if (this._settings.master)
                        return webix.$$(this._settings.master).getValue();
                    return null;
                },
                filter: function (item, value) {
                    if (item._toString().toLowerCase().indexOf(value.toLowerCase()) === 0) return true;
                    return false;
                },
                filter_setter: function (value) {
                    return webix.toFunctor(this.filter, this.$scope);
                },
            }, webix.ui.suggest);
            // Парсинг данных
            webix.DataStore.prototype.__parse = webix.DataStore.prototype._parse;
            webix.DataStore.prototype._parse = function (data, master) {
                m.data.parse(data);
                this.__parse(data, master);
            };
            // Возврат изменений после update
            webix.DataStore.prototype.updateItem = function (id, update, mode) {
                if (update && update._error) {
                    m.main.error("Запись изменений.\r\n" + update._error);
                    return;
                }
                var data = this.getItem(id);
                var old = null;

                //check is change tracking active
                var changeTrack = this.hasEvent("onDataUpdate");

                webix.assert(data, "Ivalid ID for updateItem");
                webix.assert(!update || !update.id || update.id == id, "Attempt to change ID in updateItem");
                if (!webix.isUndefined(update) && data !== update) {
                    //preserve original object
                    if (changeTrack)
                        old = webix.copy(data);

                    id = data.id;	//preserve id
                    // !!! Изменено копирование пришедших данных в существующий объект
                    //webix.extend(data, update, true);
                    m.sys.copy(data, update);
                    data.id = id;
                }

                if (this._scheme_update)
                    this._scheme_update(data);

                this.callEvent("onStoreUpdated", [id.toString(), data, (mode || "update")]);

                if (changeTrack)
                    this.callEvent("onDataUpdate", [id, data, old]);
            };
            // Подстановка своей серилизации
            webix.DataProcessor.prototype._save_inner = function (id, obj, operation) {
                if (typeof id == "object") id = id.toString();
                if (!id || this._ignore === true || !operation || operation == "paint") return true;

                var store = this._settings.store;

                // 
                obj = m.data.serialize(obj);

                if (store && store._scheme_serialize)
                    obj = store._scheme_serialize(obj);

                var update = { id: id, data: this._copy_data(obj), operation: operation };
                //save parent id
                if (!webix.isUndefined(obj.$parent)) update.data.parent = obj.$parent;

                if (update.operation != "delete") {
                    //prevent saving of not-validated records
                    var master = this._settings.master;
                    if (master && master.data && master.data.getMark && master.data.getMark(id, "webix_invalid"))
                        update._invalid = true;

                    if (!this.validate(null, update.data))
                        update._invalid = true;
                }

                if (this._check_unique(update))
                    this._updates.push(update);

                if (this._settings.autoupdate)
                    this.send();
                // BIND !!!!!!!!!!!!!!!!!!! /////////
                m.commands.bind(this);
                /////////////////////////////////////
                return true;
            };
            /*
            webix.editors.obj = webix.extend({
                getValue: function () {
                    return '123'
                },
                setValue: function (value) {
 
                }
            }, webix.editors.text);
            */
            // activeContent
            //webix.protoUI({
            //    name: "alist"
            //}, webix.ui.list, webix.ActiveContent)
        }
    },
    // Перечитать настройки
    refreshApp: function () {
        let result = true;
        // Загрузить макрокоманды
        try {
            let js = m.macro.prepare(webix.ajax().sync().get("sys/main/macros").responseText);
        }
        catch (e) {
            m.main.error('Ошибки при трансляции макро (' + e.message + ')');
            result = false;
        }
        // Загрузить команды
        try {
            let js = m.macro.prepareCommands(webix.ajax().sync().get("sys/main/commands").responseText);
        }
        catch (e) {
            m.main.error('Ошибки при трансляции макро (' + e.message + ')');
            result = false;
        }
        // Загрузить описание классов модели
        try {
            m.macro.prepareModel(webix.ajax().sync().get("sys/main/model").responseText);
        }
        catch (e) {
            m.main.error('Ошибки при трансляции классов модели (' + e.message + ')');
            result = false;
        }
        // Загрузить описание базовых классов модели
        try {
            m.macro.prepareBase();
        }
        catch (e) {
            m.main.error('Ошибки при трансляции базовых классов модели (' + e.message + ')');
            result = false;
        }
        // Загрузить конфигурацию
        try {
            m.data.loadCfg(webix.ajax().sync().get("sys/main/cfg").responseText);
        }
        catch (e) {
            m.main.error('Ошибки при трансляции классов модели (' + e.message + ')');
            result = false;
        }
        return result;
    },
    // Загрузить описание формы(окна) для webix
    getAppJson: function (command, params, folder) {
        let json = null;
        let uid = m.newUid();
        try {
            if (!folder) folder = 'main';
            // Получить описание из макрокоманды
            if (!params) params = {};
            params._uid_ = uid;
            json = m.macro.run('$' + m.sys.webixName(command), params, true);
            // Если нашли описание команды
            if (typeof (json) == 'object' && json.command) json = null;
            // Получить описание в контроллере приложений
            if (m.sys.isEmpty(json)) {
                let text = webix.ajax().sync().get("app/" + folder + "/" + command).responseText;
                json = eval(m.sys.replaceId(text, uid));
                json = m.macro.parse(json, params);
            }
        }
        catch (e) {
            m.main.error('Ошибки при подготовки команды ' + command + ' (' + e.message + ')' /* + '\r\n' + e.stack*/);
        }
        return json;
    },
    // Перечитать все
    refreshAll: function () {
        this.customWebix();
        if (this.refreshApp()) {
            let json = m.main.getAppJson('main');
            json.id = "_main_";
            webix.ui.fullScreen();
            m.commands.prepareBind(webix.ui(json, $$('_main_')));
            if (json.onLoadJson) json.onLoadJson({});
        }
    },
    // Добавить новую страницу
    showTab: function (commandName, header, createButton) {
        let mainViews = $$("mainviews");
        let tabId = m.main.tabId(commandName);
        let butId = m.main.butId(commandName);
        let view = $$(tabId);
        // Сформировать содежимое закладки если ее не нашлм
        if (!view) {
            let params = {};
            let json = m.main.getAppJson(commandName, params);
            params.content = json;
            params.name = commandName;
            let page = m.main.getAppJson('page', params);
            page.id = tabId;

            mainViews.addView(page);
            m.commands.prepareBind(tabId);
            if (json.onLoadJson) json.onLoadJson({});
            view = $$(tabId)
            view._json_ = json;  // для отладки чтобы смотреть конфигурацию
        }
        // Сформировать кнопку для вызова в главном меню если ее еще нет
        if (createButton && !$$(butId) && !$$('_' + butId)) {
            let mainBar = $$("mainbar");
            let json2 = {};
            json2.id = butId;
            json2.view = "button";
            //json2.type = 'icon';
            json2.type = 'image';
            json2.css = 'main-but';
            json2.align = 'left';
            json2.width = 48;
            json2.click = function () { m.main.runCommand(commandName, null) };
            command = m.commands.findCommand(commandName);
            if (command) {
                //json2.icon = command.icon;
                json2.image = 'images/main/icons8-' + command.icon + '.png',
                json2.tooltip = m.sys.def(command.tooltip, command.label);
            }
            mainBar.addView(json2, mainBar.index($$('mainlastindex')));
        }
        // Показать закладку
        view.show();
       this.strechLayouts(view);
    },
    // Растянуть Layoutы
    strechLayouts: function (view, parent) {
        if (parent && view.config.height == -1) {
            view.config.height = parent.$height || parent.config.height;
        }
        let childs = view.getChildViews();
        for (let i in childs) {
            this.strechLayouts(childs[i], view);
        }
    },
    // Загрузить описание страницы и добавить ее в область страниц
    runCommand: function (command, params, createButton) {
        try {
            m.main.showTab(command, params, createButton);
        }
        catch (e) {
            m.main.error('Ошибки при выполнении команды ' + command + ' (' + e.message + ')' + '\r\n' + e.stack);
        }
    },
    // Id страницы
    tabId: function (command) {
        if (command && command.substring(0, 5) != '_tab_') command = '_tab_' + command;
        return command;
    },
    // Id кнопки для страницы
    butId: function (command) {
        if (command && command.substring(0, 5) != '_but_') command = '_but_' + command;
        return command;
    },
    // Удалить закладку
    removeTab: function (command) {
        let tabId = null;
        let butId = null;
        if (command) {
            tabId = this.tabId(command);
            butId = this.butId(command);
        }
        else {
            tabId = $$('mainviews').getValue();
            butId = tabId.replace('_tab_', '_but_');
        }

        if (tabId) {
            if ($$(tabId)) $$('mainviews').removeView(tabId);
        }
        if (butId) {
            if ($$(butId)) $$('mainbar').removeView(butId);
        }
    },
    // Проверка закладки
    hasTab: function (command) {
        return $$(this.tabId(command)) != null;
    },
    // Выполнить диалог
    runDialog: function (command, params) {

        try {
            if (!params) params = {};
            let json = m.main.getAppJson(command, params);
            let dlg = null;
            if (json.view == 'window') {
                dlg = webix.ui(json);
            }
            else {
                params.content = json;
                params.name = params.command || command;
                let wnd = m.main.getAppJson('window', params);
                dlg = webix.ui(wnd);
            }
            dlg.show();
            m.params.set(dlg, params);
            m.commands.prepareBind(dlg);
            if (json.onLoadJson) json.onLoadJson(params);
            dlg._json_ = json;  // для отладки чтобы смотреть конфигурацию
        }
        catch (e) {
            m.main.error('Ошибки при выполнении команды ' + command + ' (' + e.message + ')');
        }
    },
    close: function (controlObj) {
        while (controlObj) {
            let parentObj = controlObj.getParentView();
            if (controlObj.config.view == 'window') {
                controlObj.close();
                return;
            }
            else if (controlObj.config.id && controlObj.config.id.substr(0, 5) == '_tab_') {
                m.main.removeTab(controlObj.config.id.substring(5))
            }
            controlObj = parentObj;
        }
    },
    // Сообщение об ошибке
    error: function (text) {
        webix.message({
            type: "error",
            text: text,
            expire: -1,
            width: 400
        });
    },
    // Alert
    alert: function (text, title) {
        if (!title) title = "Ошибка..."
        webix.alert({
            title: title,
            text: text,
            type: "alert-error"
        })
    },
};