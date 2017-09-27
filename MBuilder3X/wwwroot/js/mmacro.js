if (typeof (m) == 'undefined') m = {};
///////////////////////////////////////////////////////////////////////////
// Поддерка макро
///////////////////////////////////////////////////////////////////////////
m.macro = {
    // Общие переменные
    _macros: {},                // список макрокоманд
    _commands: {},              // список команд
    _baseClasses: {},           // список базовых классов
    _modelClasses: {},          // сисок классов модели
    // Начитать классы модели
    prepareModel: function (text) {
        try {
            let o = eval('(' + text + ')');
            this.addModels(o);
        }
        catch (e) {
            m.main.error('Ошибки при трансляции модели (' + e.message + ')' + '\r\n' + t);
        }
    },
    addModels: function (js) {
        for (let n in js) {
            let o = js[n];
            if (typeof (o) == 'object') {
                switch (o._mtype) {
                    case 'class':
                        let o1 = {};
                        for (let n1 in o) {
                            if (o[n1]._mtype == 'prop') {
                                o1[m.sys.webixName(n1)] = o[n1];
                            }
                            else {
                                o1[n1] = o[n1];
                            }
                        }
                        this._modelClasses[n] = o1;
                        break;
                }
            }
        }
    },
    // Подготовить базовые классы и описателей модели
    prepareBase: function () {
        this._baseClasses = {};
        // Пройдем по модели и сформируем базовые классы для данных 
        // сама модель суть описатель
        for (let name in this._modelClasses) {
            let srcClass = this._modelClasses[name];
            let baseClass = {};
            baseClass.__base__ = srcClass._base;
            for (let pname in srcClass) {
                let srcProp = srcClass[pname];
                // Метод
                if (typeof (srcProp) == 'function') {
                    baseClass[pname] = srcProp;
                }
                // Геттер или сеттер
                else if (typeof (srcProp) == 'object' && (typeof (srcProp.get) == 'function' || typeof (srcProp.set) == 'function')) {
                    Object.defineProperty(baseClass, pname, srcProp);
                }
                // Свойство 
                else if (srcProp._mtype == 'prop') {
                    let datatype = srcProp._type;
                    // Ссылка на объект
                    if (this._modelClasses[datatype]) {
                        if (srcProp._ptype) {
                            switch (srcProp._ptype.toUpperCase()) {
                                case 'REF':
                                case 'CFG':
                                    Object.defineProperty(baseClass, pname, {
                                        get: m.parse.getFunc('function(){return m.data.getObj(this, "' + datatype + '", "' + pname + 'Id")}'),
                                        set: m.parse.getFunc('function(value){return m.data.setObj(this, value, "' + datatype + '", "' + pname + 'Id")}')
                                    });
                                    break;
                                case 'LIST':
                                    Object.defineProperty(baseClass, pname, {
                                        get: m.parse.getFunc('function(){return m.data.getList(this, "' + datatype + '", "' + pname + '")}'),
                                    });
                                    break;
                                default:
                                    m.main.error('Метод prepareBase. Свойство _ptype=' + srcProp._ptype + ' не определено...');
                                    break;
                            }
                        }
                    }
                    else if (datatype) {
                        //baseClass[pname] = this.getDefault(datatype);
                        baseClass[pname] = srcProp._default;
                    }
                }
            }
            this._baseClasses[name] = baseClass;
        }
        // Проставим прототипы для всех объектов
        for (let name in this._baseClasses) {
            let baseClass = this._baseClasses[name];
            let proto = this._baseClasses[baseClass.__base__];
            if (!proto) {
                proto = m.base[baseClass.__base__];
            }
            if (!proto) {
                m.main.error('Ошибки при подготовке базовых классов. Для класса [' + name + '] базовый класс [' + baseClass.__base__ + '] не найден');
            }
            baseClass.__proto__ = proto;
        }
    },
    getDefault: function (datatype) {
        let result = "";
        if (datatype) {
            switch (datatype.toLowerCase()) {
                case "bool":
                    result = false;
                    break;
                case "int":
                case "short":
                case "float":
                case "double":
                case "decimal":
                case "char":
                case "byte":
                    result = 0;
                    break;
                case "datetime":
                    result = "1753-01-01T12:00:00";
                    break;
                case "guid":
                    result = "00000000-0000-0000-0000-000000000000";
                    break;
            }
        }
        return result;
    },
    // Получить базовый класс по имени
    getBaseClass: function (type) {
        let baseClass = this._baseClasses[type];
        if (!baseClass) baseClass = {};
        return baseClass
    },
    // Получить описатель класса по имени
    getModelClass: function (type) {
        let modelClass = this._modelClasses[type];
        if (!modelClass) modelClass = {};
        return modelClass
    },
    // Добавить макро в список
    add: function (macros) {
        for (let n in macros) {
            this._macros[n] = macros[n];
        }
    },
    // Добавить макро в список
    addCommand: function (commands) {
        for (let n in commands) {
            this._commands[n] = commands[n];
        }
    },
    // Добавить макро в список
    prepare: function (text) {
        let l = ('});' + text + '({').split(/\x7d\x29;\s*\x28\x7b/);
        for (let i in l) {
            let t = l[i];
            if (t) {
                try {
                    let o = eval('({' + t + '})');
                    this.add(o);
                }
                catch (e) {
                    let n = t.match(/\$\w+/g)[0];
                    m.main.error('Ошибки при трансляции макро ' + n + ' (' + e.message + ')');
                }
            }
        }
    },
    prepareCommands: function (text) {
        let l = ('});' + text + '({').split(/\x7d\x29;\s*\x28\x7b/);
        for (let i in l) {
            let t = l[i];
            if (t) {
                try {
                    let o = eval('({' + t + '})');
                    this.addCommand(o);
                }
                catch (e) {
                    let n = t.match(/\$\w+/g)[0];
                    m.main.error('Ошибки при трансляции команды ' + n + ' (' + e.message + ')');
                }
            }
        }
    },
    // Получить макро по имени
    getMacro: function (name) {
        if (typeof (name) == 'string') {
            if (name.substring(0, 1) != '$') name = '$' + name;
            return this._macros[name];
        }
        if (typeof (name) == 'object') return name;
        return null;
    },
    // Получить команду по имени
    getCommand: function (name) {
        if (typeof (name) == 'string') {
            if (name.substring(0, 1) != '$') name = '$' + name;
            return this._commands[name];
        }
        if (typeof (name) == 'object') return name;
        return null;
    },
    // Выполнить макро
    run: function (name, parms, hideError) {
        if (!parms) parms = {};
        // Если значением параметра является макро то подставим ее
        if (typeof (parms == 'object')) {
            for (let pn in parms) {
                let pv = parms[pn];
                if (typeof (pv) == 'string' && pv.substring(0, 1) == '$') {
                    parms[pn] = m.macro.run(pv);
                }
            }
        }
        let val = {};
        try {
            let macro = this.getMacro(name);
            if (!macro) macro = this.getCommand(name);
            if (macro) {
                let type = typeof (macro);
                if (type == 'function') {
                    //parms._uid_ = m.sys.uid();
                    val = macro(parms);
                }
                else if (type == "object") {
                    val = macro;
                }
                else {
                    m.main.error('Макро ' + name + '(type) не может быть использована в данном контексте');
                    return result;
                }
                val = this.parse(val, parms);
            }
            else if (!hideError) {
                m.main.error('Макро ' + name + ' не найдена');
            }
        }
        catch (e) {
            m.main.error("Ошибки при вычислении макро " + name + " (" + e.message + ") ...");
            return "";
        }

        return val;
    },
    // Вернуть функцию по строке
    getFunc: function (func) {
        let f = new Function("return " + func);
        return f();
    },
    // Параметры по умолчанию 
    setDefaults: function (parms, defaults) {
        for (let n in defaults) {
            if (parms[n] == null) parms[n] = defaults[n];
        }
    },
    // Парсить исходный json с подстановкой макро
    parse: function (json, parms) {
        // Если значением параметра является макро то подставим ее
        if (typeof (parms == 'object')) {
            for (let pn in parms) {
                let pv = parms[pn];
                if (typeof (pv) == 'string' && pv.substring(0, 1) == '$' && pv != '$$' && pv != '$init' && pv != '$update' && pv != '$save' && pv != '$change' && pv != '$serialize' && pv != '$group' && pv != '$sort') {
                    parms[pn] = m.macro.run(pv);
                }
            }
        }

        let result = {};
        try {
            let t = typeof (json);
            if (Array.isArray(json)) t = "array";
            switch (t) {
                case "object": {
                    for (let name in json) {
                        // Сообщение об ошибке
                        if (name == '_error_') {
                            m.main.error(json[name])
                            return result;
                        }
                        // Макро $content
                        if (name == '$content') {
                            let val = json[name];
                            let condition = val['?'];
                            if (typeof (condition) == 'undefined' || condition) {
                                if (typeof (val) == 'function') {
                                    val = val(parms);
                                }
                                val = this.parse(val, parms);
                                if (val) {
                                    condition = val['?'];
                                    if (typeof (condition) == 'undefined' || condition) {
                                        for (let name1 in val) {
                                            if (name1 != '?') {
                                                let val1 = val[name1];
                                                if (val1 != null) result[name1] = val1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        // Встречена макрокоманда
                        else if (name.substring(0, 1) == '$' && name != '$$' && name != '$init' && name != '$update' && name != '$save' && name != '$change' && name != '$serialize' && name != '$group' && name != '$sort') {
                            // Параметры макро
                            let parms1 = json[name];
                            if (parms1 && parms) {
                                //if (parms1.__proto__ === parms.__proto__) parms1.__proto__ = null;
                                parms1.__proto__ = parms;
                            }
                            let val = this.run(name, parms1);
                            if (val) {
                                for (let name1 in val) {
                                    let val1 = val[name1];
                                    if (val1 != null) result[name1] = val1;
                                }
                            }
                        }
                        else {
                            let val = json[name];
                            if (typeof (val) == 'string' && val.substring(1, 9) == 'function(') {
                                val = m.parse.getFunc(val);
                            }
                            val = this.parse(val, parms);
                            if (val != null) result[name] = val;
                        }
                    }
                    break;
                }
                case "array": {
                    let array = [];
                    for (let i = 0; i < json.length; i++) {
                        let val = this.parse(json[i], parms);
                        array.push(val);
                    }
                    return array;
                }
                default: {
                    if (!isNaN(json)) {
                        json = eval(json);
                    }
                    else if (json == 'False') {
                        json = false;
                    }
                    else if (json == 'True') {
                        json = true;
                    }
                    else if (t == 'string' && json.substring(0, 9) == "function(") {
                        json = this.getFunc(json);
                    }
                    return json;
                }

            }
        }
        catch (e) {
            m.main.error("Ошибки при разборе " + m.macro.jsonToString(json) + " (" + e.message + ") ...");
            throw e;
        }
        return result;
    },
    // Вернуть json как строку
    jsonToString: function (json, level) {
        if (level == null) level = "";
        else level = level + "    ";
        let s = '';
        let t = typeof (json);
        if (Array.isArray(json)) t = "array";
        switch (t) {
            case "object": {
                s += '{\r\n';
                let d = '';
                for (let n in json) {
                    s += level + d + '"' + n + '"' + ':';
                    s += this.jsonToString(json[n], level);
                    d = ',';
                }
                s += level.substring(4) + '}\r\n';
                break;
            }
            case "array": {
                s += '[\r\n';
                let d = '';
                for (let i in json) {
                    s += level + d + this.jsonToString(json[i], level);
                    d = ',';
                }
                s += level.substring(4) + ']\r\n';
                break;
            }
            case "function": {
                s += json + '\r\n';
                break;
            }
            default: {
                if (typeof (json) == 'string') {
                    s += '"' + json + '"' + '\r\n';
                }
                else {
                    s += json + '\r\n';
                }
                break;
            }
        }
        return s;
    }
};