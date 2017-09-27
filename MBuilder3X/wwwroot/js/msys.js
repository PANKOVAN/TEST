if (typeof (m) == 'undefined') m = {};
///////////////////////////////////////////////////////////////////////////
// SYS
///////////////////////////////////////////////////////////////////////////
m.sys = {
    webixName: function (name) {
        if (typeof (name) == 'string') return name.substring(0, 1).toLowerCase() + name.substring(1);
        else return name;
    },
    isEmpty: function (obj) {
        if (obj) {
            for (let i in obj) {
                return false;
            }
            return true;
        }
        return true;
    },
    copy: function (target, source) {
        if (typeof (target) == 'object' && typeof (source) == 'object') {
            for (let name in source) {
                let sourceValue = source[name];
                if (typeof (sourceValue) == 'object') {
                    // Специальный объект, задает ссылку на текущий id в заданного списка (дерева и т.д)
                    if (sourceValue.ref) {
                        ref = $$(sourceValue.ref);
                        if (ref) {
                            let item = ref.getSelectedItem();
                            if (item) {
                                let v = item[sourceValue.name || 'id'];
                                target[name] = v;
                            }
                        }
                    }
                    // Обычный объект
                    else {
                        let targetValue = target[name];
                        if (typeof (targetValue) == 'object') {
                            this.copy(targetValue, sourceValue);
                        }
                        else {
                            target[name] = sourceValue;
                        }
                    }
                }
                else {
                    target[name] = sourceValue;
                }
            }
        }
        return target;
    },
    find: function (source, func, result) {
        if (!result) {
            result = [];
        }
        if (func(source)) {
            result.push(source);
        }
        if (source) {
            switch (typeof (source)) {
                case 'object':
                    for (let n in source) {
                        this.find(source[n], func, result);
                    }
                    break;
            }
        }
        return result;
    },
    escapeRegExp: function (str) {
        return str.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
    },
    replaceAll: function (str, find, replace) {
        return str.replace(new RegExp(escapeRegExp(find), 'g'), replace);
    },
    replaceId: function (json, id) {
        if (typeof (json) == 'string') {
            if (!m.sys._regexpid) m.sys._regexpid = new RegExp('%id%', 'g');
            return json.replace(m.sys._regexpid, '_' + id);
        }
        return json;
    },
    urlAdd: function (url, name, val) {
        if (url.indexOf('?') > 0) url += '&';
        else url += '?';
        url += name + '=' + encodeURI(val);
        return url;
    },
    urlAddParams: function (url, params) {
        if (params && typeof (params) == 'object') {
            for (let n in params) {
                url = this.urlAdd(url, n, params[n])
            }
        }
        return url;
    },
    def: function (p1, p2, p3, p4, p5, p6, p7, p8) {
        if (typeof (p1) != 'undefined') return p1;
        if (typeof (p2) != 'undefined') return p2;
        if (typeof (p3) != 'undefined') return p3;
        if (typeof (p4) != 'undefined') return p4;
        if (typeof (p5) != 'undefined') return p5;
        if (typeof (p6) != 'undefined') return p6;
        if (typeof (p7) != 'undefined') return p7;
        if (typeof (p8) != 'undefined') return p8;
        return null;
    },
    // Безопасное взятие парамера(ов) от объекта значение которокого возможно=null
    // obj - объект
    // def - значение по умолчанию
    // p1-p8 - имена свойств
    get: function (obj, def, n1, n2, n3, n4, n5, n6, n7, n8) {
        let r = def;
        if (obj) {
            if (n1 && typeof (obj) == 'object') r = obj[n1];
            else return r;
            if (n2 && typeof (r) == 'object') r = r[n2];
            else return r;
            if (n3 && typeof (r) == 'object') r = r[n3];
            else return r;
            if (n4 && typeof (r) == 'object') r = r[n4];
            else return r;
            if (n5 && typeof (r) == 'object') r = r[n5];
            else return r;
            if (n6 && typeof (r) == 'object') r = r[n6];
            else return r;
            if (n7 && typeof (r) == 'object') r = r[n7];
            else return r;
            if (n8 && typeof (r) == 'object') r = r[n8];
            else return r;
        }
        return r;
    },
    treeCodeNameTemplate: function (obj, common) {
        let r = common.icon(obj, common) + common.folder(obj, common);
        if (obj.code) r += '<span class="item-code">[' + obj.code + ']</span>';
        if (obj.name) r += '<span class="item-name">' + obj.name + '</span>';
        r += '<span class="text-danger"> (' + obj.id+'/'+obj.parentId + ')</span>';
        return r;
    }
};
///////////////////////////////////////////////////////////////////////////
// UID
///////////////////////////////////////////////////////////////////////////
m._uid_ = 0;
m.bp = function () {
};
m.newUid = function () {
    this._uid_++;
    return this._uid_;
};
m.uid = function (preffix) {
    if (!preffix) preffix = 'A';
    return preffix + this.ui
};
m.getSelectedId = function (name) {
    let d = name;
    if (typeof (d) == 'string') d = $$(d);
    if (d) {
        let id = d.getSelectedId(true, true);
        if (id.length == 0) return null;
        else if (id.length == 1) return id[0];
        else return id;
    }
    else return null;
}
m.getSelectedCount = function (name) {
    let d = name;
    if (typeof (d) == 'string') d = $$(d);
    if (d) {
        return d.getSelectedId(true, true).length;
    }
    else return 0;
}
