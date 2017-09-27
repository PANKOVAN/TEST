if (typeof (m) == 'undefined') m = {};
///////////////////////////////////////////////////////////////////////////
// Парсинг 
///////////////////////////////////////////////////////////////////////////
m.parse = {
    getFunc: function (func) {
        let f = new Function("return " + func);
        return f();
    },
    getJson: function (obj) {
        for (let key in obj) {
            let val = obj[key];
            let type = typeof (val);
            // Встречена макрокоманда
            if (key == '_error_') {
                m.main.error("Ошибки на сервере " + val);
            }
            // Значением является функция
            else if (type == 'string' && val.slice(0, 9) == 'function(') {
                try {
                    let func = this.getFunc(val);
                    obj[key] = func;
                }
                catch (e) {
                    m.main.error("Ошибки на при трансляции фукции " + e.message + '\r\n' + val);
                }
            }
            // Значением является объект
            else if (type == 'object') {
                this.getJson(val);
            }
        }
    }
};