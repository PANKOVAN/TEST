if (typeof (m) == 'undefined') m = {};
///////////////////////////////////////////////////////////////////////////
// Поддерка шаблонов
///////////////////////////////////////////////////////////////////////////
m.templates = {
    // GetTemplate
    templatesList: {},
    runTemplate: function (t, obj, url) {
        let v = "";
        try {
            v = t(obj);
        }
        catch (e) {
            m.main.error("Ошибки при вычислении шаблона " + url + " (" + e.message + ") ...");
            return "";
        }
        return v;
    },
    getTemplate: function (url, obj) {
        let t = this.templatesList[url];
        if (t) return this.runTemplate(t, obj, url);
        try {
            t = webix.ajax().sync().get(url).responseText;
            if (t) {
                this.templatesList[url] = m.parse.getFunc(t);
                t = this.templatesList[url];
                if (t) return this.runTemplate(t, obj, url);
            }
            else {
                m.main.alert("Шаблона " + url + " не найден...", "Загрузка шаблона...");
                this.templatesList[url] = m.parse.getFunc("function(){return ''}");
                return "";
            }
        }
        catch (e) {
            m.main.alert("<div>Ошибки при трансляции шаблона " + url + "<br/>" + e.message + "<br/>" + t.replace("+", "+\r\n</div>"), "Трансляция шаблона...");
            this.templatesList[url] = m.parse.getFunc("function(){return ''}");
            return "";
        }
    },
    _isFirst: false,
    _isNext: false,
    _value: function (obj, value, params, saveHtml) {
        if (!value) return "";
        if (!saveHtml) {
            let t = typeof (value);
            if (t == "string") {
                value = value.replace("<", "").replace("<", "").replace("&", "");
            }
        }
        let pref = params["pref"];
        let suf = params["suf"];
        let first = params["first"];
        let next = params["next"];

        if (pref) value = pref + value;
        if (suf) value = value + suf;
        if (first) {
            this._isFirst = true;
            this._isNext = false;
        }
        if (next) {
            if (this._isFirst) {
                value = next + value;
            }
            else {
                if (this._isNext) {
                    value = next + value;
                }
            }
            this._isFirst = false;
            this._isNext = true;
        }
        return value;
    },
    _html: function (obj, value, params) {

        return this.value(obj, value, params, true);
    },
    _if: function (obj, value, params) {
        return value;
    },
    _switch: function (obj, value, params) {
        return value;
    },
    _for: function (obj, value, params) {
        return value;
    },
    _run: function (obj, value, params) {
        if (value) {
            if (value.indexOf("/main") <= 0) value = "main/" + value;
            if (value.indexOf("templates/") <= 0) value = "templates/" + value;
            return this.getTemplate(value, obj);
        }
        return "";
    }
};