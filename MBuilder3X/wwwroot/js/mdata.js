if (typeof (m) == 'undefined') m = {};
///////////////////////////////////////////////////////////////////////////
// Источники данных
///////////////////////////////////////////////////////////////////////////
m.data = {
    // Словарь /////////////////////////
    _cfgDic: {},
    _curDic: {},
    setCurDic: function (dic) {
        this._curDic = dic;
    },
    get: function (type, id) {
        if (typeof (id) == 'string') id = id.toUpperCase();
        let dic = this._cfgDic[type];
        if (!dic) dic = this._curDic[type];
        if (dic) return dic[id];
        return null;
    },
    set: function (type, id, value, isCfg) {
        if (typeof (id) == 'string') id = id.toUpperCase();
        let dic = null;
        if (isCfg) {
            dic = this._cfgDic[type];
            if (!dic) {
                dic = {};
                this._cfgDic[type] = dic;
            }
        }
        else {
            dic = this._curDic[type];
            if (!dic) {
                dic = {};
                this._curDic[type] = dic;
            }
        }
        if (!dic[id]) dic[id] = value;
        else m.sys.copy(dic[id], value);
    },
    ////////////////////////////////////
    beforeAdd: function (id, obj, index) {
        obj._pos = index;
    },
    parse: function (data) {
        if (data) {
            let error = data._error;
            if (error) {
                m.main.error("Ошибки на сервере " + error);
            }
            if (data) {
                // Прочитать данные
                if (data.data) {
                    for (let i = 0; i < data.data.length; i++) {
                        // Стиль
                        let obj = data.data[i];
                        if (obj.webix_css) {
                            obj.$css = obj.webix_css;
                        }
                        // Установить прототипы и ссылки на объекты
                        this.setProto(obj, null, data);
                    }
                }
                // А тепереь словарь
                if (data.dic) {
                    for (let type in data.dic) {
                        let proto = m.macro.getBaseClass(type);
                        if (proto) {
                            let l = data.dic[type];
                            for (let id in l) {
                                this.setProto(l[id], id, data);
                            }
                        }
                    }
                }
            }
            //if (data.dataMode == 'tree') {
            //    this.parseTree(data);
            //}
        }
    },
    // Установить прототип и положить в словарь
    setProto: function (obj, id) {
        if (obj._isproto) return obj;
        obj._isproto = true;
        if (typeof (obj.id) != 'undefined') id = obj.id;
        let type = obj._type;
        if (type && typeof (id) != 'undefined') {
            // Прототип
            let proto = m.macro.getBaseClass(type);
            obj.__proto__ = proto;
            // Положить в словарь если это объект конфигурации
            m.data.set(type, id, obj, proto._isCfg)
            // Установить ссылки
            for (let i in obj) {
                let v = obj[i];
                if (typeof (v) == 'object') {
                    if (v['_type']) {
                        obj[i] = this.getProto(v);
                        this.setProto(v, null);
                    }
                    else {
                        for (let j in v) {
                            let v1 = v[j];
                            if (typeof (v1 == 'object')) {
                                if (v1['_type']) {
                                    v[j] = this.getProto(v1);
                                    this.setProto(v1, null);
                                }
                            }
                        }
                    }
                }
            }
        }
        return obj;
    },
    getProto: function (obj) {
        let id = obj.id;
        let type = obj._type;
        if (type && id) {
            // Найти в словаре
            let obj = m.data.get(type, id);
            if (!obj) {
                obj = this.setProto({ _type: type, id: id });
            }
            return obj;
        }
        return null;
    },
    serialize: function (obj) {
        let obj1 = {}
        for (let n in obj) {
            if (obj.hasOwnProperty(n)) {
                let v = obj[n];
                let t = typeof (v);
                //if (v == null || (typeof (v) == 'object' && v['_type'])) continue;
                if (t != 'object' && t != 'function') {
                    obj1[n] = v;
                }
            }
        }
        return obj1;
    },
    getObj: function (self, type, idname) {
        if (self) {
            return m.data.get(type, self[idname]);
        }
        return null;
    },
    setObj: function (self, value, type, name) {
        if (self) {
            self[idname] = value['id'];
        }
    },
    getList: function (self, type, name) {
        if (self) {
            name = '__' + name;
            if (!self[name]) self[name] = [];
            return self[name];
        }
        return null;
    },
    newObj: function (type, obj) {
        if (!obj) obj = {};
        obj._type = type;
        obj.__proto__ = m.macro.getBaseClass(type);
        return obj;
    },
    loadCfg: function (data) {
        this.parse(eval('(' + data + ')'));
    },
    /*
    parseTree: function (data) {
        let data1 = [];
        for (let i in data.data) {
            let o = data.data[i];
            o.$css = 'item-tree';
            if (o.parentId != 0) {
                let p = o.parent;
                if (p) {
                    if (typeof (p.data) == 'undefined') p.data = [];
                    p.data.push(o);
                }
            }
            else {
                data1.push(o);
            }
        }
        data.data = data1;
    }
    */
};