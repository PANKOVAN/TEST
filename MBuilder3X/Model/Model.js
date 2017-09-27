{
    //////////////////////////////////////////////////////////////////////////////////////////
    //  [MKF] ЦНТИ-МЕКОФ               (10.10.2017 8:42:59)
    //////////////////////////////////////////////////////////////////////////////////////////
    MDocGroup: { // Группа  --------------------------------------------------
        _mtype: "class",
            _header: "Группа ",
                _base: "MEDMObj",
                    _table: "DocGroup",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Code: { // Код .......................................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        RCode: { // Код шаблона ..............................................
            _mtype: "prop",
                _header: "Код шаблона",
                    _type: "string",
                        _default: "",
                            label: "Код шаблона",
                                _element: {
                view: "combo",
                    options: {
                    view: "suggest",
                        body: {
                        view: "list",
                            url: "templates/list?path=docs"
                    }
                }
            }
        },
        Name: { // Наименование ..............................................
            _mtype: "prop",
                _header: "Наименование",
                    _type: "string",
                        _default: "",
                            label: "Наименование"
        },
        Rem: { // Примечания .................................................
            _mtype: "prop",
                _header: "Примечания",
                    _type: "string",
                        _default: "",
                            label: "Примечания"
        },
        _toString: function () {
            return '[' + this.code + '] ' + this.name;
        },
        webix_kids: {
            get: function () {
                return true;
            },
            set: function (value) {
            }
        }
    },
    MDocType: { // Тип документа ---------------------------------------------
        _mtype: "class",
            _header: "Тип документа",
                _base: "MEDMObj",
                    _table: "DocType",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Code: { // Код .......................................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        RCode: { // Код шаблона ..............................................
            _mtype: "prop",
                _header: "Код шаблона",
                    _type: "string",
                        _default: "",
                            label: "Код шаблона",
                                _element: {
                view: "combo",
                    options: {
                    view: "suggest",
                        body: {
                        view: "list",
                            url: "templates/list?path=docs"
                    }
                }
            }
        },
        Name: { // Наименование ..............................................
            _mtype: "prop",
                _header: "Наименование",
                    _type: "string",
                        _default: "",
                            label: "Наименование"
        },
        Rem: { // Примечания .................................................
            _mtype: "prop",
                _header: "Примечания",
                    _type: "string",
                        _default: "",
                            label: "Примечания"
        },
        DocGroup: { // Группа ................................................
            _mtype: "prop",
                _type: "MDocGroup",
                    _default: { },
            label: "Группа",
                //labelWidth: 100,
                //labelAlign: 'right',
                labelPosition: 'top',
                    _element: {
                view: "obj",
                    suggest: {
                    view: "suggestObj",
                        body: {
                        view: "list",
                            url: "data/main/DocGroup",
                                template: "[#code#] #name#"
                    }
                }
            }
        },
        Fields: { // Список полей ............................................
            _mtype: "prop",
                _header: "Список полей",
                    _type: "string",
                        _default: "",
                            label: "Список полей"
        },
        _toString: function () {
            return '[' + this.code + '] ' + this.name;
        },
        webix_kids: {
            get: function() {
                return false;
            },
            set: function(value) {
            }
        }
    },
    MDoc: { // Документ ------------------------------------------------------
        _mtype: "class",
            _header: "Документ",
                _base: "MEDMObj",
                    _table: "Doc",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        DocType: { // Тип документа ..........................................
            _mtype: "prop",
                _type: "MDocType",
                    _default: { },
            label: "Тип документа"
        },
        DateRecordSystem: { // Дата ввода записи .............................
            _mtype: "prop",
                _header: "Дата ввода записи",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата ввода записи"
        },
        DateShedulingRecord: { // Дата составления записи ....................
            _mtype: "prop",
                _header: "Дата составления записи",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата составления записи"
        },
        Abstract: { // Реферат (аннотация) ...................................
            _mtype: "prop",
                _header: "Реферат (аннотация)",
                    _type: "string",
                        _default: "",
                            label: "Реферат (аннотация)"
        },
        IndexUDC: { // Индекс УДК ............................................
            _mtype: "prop",
                _header: "Индекс УДК",
                    _type: "string",
                        _default: "",
                            label: "Индекс УДК"
        },
        Pages: { // Кол-во страниц ...........................................
            _mtype: "prop",
                _header: "Кол-во страниц",
                    _type: "string",
                        _default: "",
                            label: "Кол-во страниц"
        },
        Pictures: { // Кол-во иллюстраций ....................................
            _mtype: "prop",
                _header: "Кол-во иллюстраций",
                    _type: "string",
                        _default: "",
                            label: "Кол-во иллюстраций"
        },
        OrganCreatorRecord: { // Создатель записи ............................
            _mtype: "prop",
                _header: "Создатель записи",
                    _type: "string",
                        _default: "",
                            label: "Создатель записи"
        },
        IndividualAuthor: { // Автор .........................................
            _mtype: "prop",
                _header: "Автор",
                    _type: "string",
                        _default: "",
                            label: "Автор"
        },
        Compiler: { // Составитель ...........................................
            _mtype: "prop",
                _header: "Составитель",
                    _type: "string",
                        _default: "",
                            label: "Составитель"
        },
        CipherSpreading: { // Шифр распространения ...........................
            _mtype: "prop",
                _header: "Шифр распространения",
                    _type: "string",
                        _default: "",
                            label: "Шифр распространения"
        },
        RecordIdentifier: { // Номер документа ...............................
            _mtype: "prop",
                _header: "Номер документа",
                    _type: "string",
                        _default: "",
                            label: "Номер документа"
        },
        EquippingReferences: { // Оснащение ссылками .........................
            _mtype: "prop",
                _header: "Оснащение ссылками",
                    _type: "string",
                        _default: "",
                            label: "Оснащение ссылками"
        },
        Name: { // Заглавие ..................................................
            _mtype: "prop",
                _header: "Заглавие",
                    _type: "string",
                        _default: "",
                            label: "Заглавие"
        },
        Picture: { // Рисунок ................................................
            _mtype: "prop",
                _header: "Рисунок",
                    _type: "Stream",
                        _default: "",
                            label: "Рисунок"
        },
        LanguageMainText: { // Язык публикации ...............................
            _mtype: "prop",
                _type: "MLanguage",
                    _default: { },
            label: "Язык публикации"
        },
        LanguageTranslation: { // Язык перевода ..............................
            _mtype: "prop",
                _type: "MLanguage",
                    _default: { },
            label: "Язык перевода"
        },
        PlacePublishingCountry: { // Страна издания ..........................
            _mtype: "prop",
                _type: "MCountry",
                    _default: { },
            label: "Страна издания"
        },
        CountryPresentingDemand: { // Страна запроса  ........................
            _mtype: "prop",
                _type: "MCountry",
                    _default: { },
            label: "Страна запроса "
        },
        NatureWork: { // Тип работ ...........................................
            _mtype: "prop",
                _type: "MTypeJob",
                    _default: { },
            label: "Тип работ"
        },
        TypePatent: { // Тип патента .........................................
            _mtype: "prop",
                _type: "MTypePatent",
                    _default: { },
            label: "Тип патента"
        },
        NatureDocument: { // Вид документа ...................................
            _mtype: "prop",
                _type: "MDocChar",
                    _default: { },
            label: "Вид документа"
        },
        DatePublishing: { // Дата издания ....................................
            _mtype: "prop",
                _header: "Дата издания",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата издания"
        },
        DateCompletionsActionNTD: { // Дата окончания действия  НТД ..........
            _mtype: "prop",
                _header: "Дата окончания действия  НТД",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата окончания действия  НТД"
        },
        AbstractJournalName: { // Реферативный журнал ........................
            _mtype: "prop",
                _header: "Реферативный журнал",
                    _type: "string",
                        _default: "",
                            label: "Реферативный журнал"
        },
        InventoryReportNumber: { // Номер ИК (ИЛ), отчета НИР, диссертации ...
            _mtype: "prop",
                _header: "Номер ИК (ИЛ), отчета НИР, диссертации",
                    _type: "string",
                        _default: "",
                            label: "Номер ИК (ИЛ), отчета НИР, диссертации"
        },
        ThatPartMuchthat: { // Том (часть многотомника) ......................
            _mtype: "prop",
                _header: "Том (часть многотомника)",
                    _type: "string",
                        _default: "",
                            label: "Том (часть многотомника)"
        },
        CodeISBN: { // Код ISBN ..............................................
            _mtype: "prop",
                _header: "Код ISBN",
                    _type: "string",
                        _default: "",
                            label: "Код ISBN"
        },
        NumberDemandInvention: { // Номер заявки на изобретения ..............
            _mtype: "prop",
                _header: "Номер заявки на изобретения",
                    _type: "string",
                        _default: "",
                            label: "Номер заявки на изобретения"
        },
        CodeOKPOSovet: { // Код организации (защиты) .........................
            _mtype: "prop",
                _header: "Код организации (защиты)",
                    _type: "string",
                        _default: "",
                            label: "Код организации (защиты)"
        },
        ConditionsAquisitionDoc: { // Условия приобретения документа .........
            _mtype: "prop",
                _header: "Условия приобретения документа",
                    _type: "bool",
                        _default: false,
                            label: "Условия приобретения документа"
        },
        OrganTranslator: { // Организация-переводчик .........................
            _mtype: "prop",
                _header: "Организация-переводчик",
                    _type: "string",
                        _default: "",
                            label: "Организация-переводчик"
        },
        NameOrganDepositor: { // Наименование организации депонента ..........
            _mtype: "prop",
                _header: "Наименование организации депонента",
                    _type: "string",
                        _default: "",
                            label: "Наименование организации депонента"
        },
        DepositionNumber: { // Номер депонирования ...........................
            _mtype: "prop",
                _header: "Номер депонирования",
                    _type: "string",
                        _default: "",
                            label: "Номер депонирования"
        },
        PlacePublishingCity: { // Место издания (страна) .....................
            _mtype: "prop",
                _header: "Место издания (страна)",
                    _type: "string",
                        _default: "",
                            label: "Место издания (страна)"
        },
        CodeISSN: { // Код ISSN ..............................................
            _mtype: "prop",
                _header: "Код ISSN",
                    _type: "string",
                        _default: "",
                            label: "Код ISSN"
        },
        ScientificDegree: { // Ученая степень ................................
            _mtype: "prop",
                _header: "Ученая степень",
                    _type: "string",
                        _default: "",
                            label: "Ученая степень"
        },
        OrganPlaceProtectThesis: { // Организация защиты диссертации .........
            _mtype: "prop",
                _header: "Организация защиты диссертации",
                    _type: "string",
                        _default: "",
                            label: "Организация защиты диссертации"
        },
        SiteOrganTransCity: { // Местонахождение переводчика (город) .........
            _mtype: "prop",
                _header: "Местонахождение переводчика (город)",
                    _type: "string",
                        _default: "",
                            label: "Местонахождение переводчика (город)"
        },
        NameSubjectNIR: { // Наименование темы НИР ...........................
            _mtype: "prop",
                _header: "Наименование темы НИР",
                    _type: "string",
                        _default: "",
                            label: "Наименование темы НИР"
        },
        IndexNKI: { // Индекс НКИ ............................................
            _mtype: "prop",
                _header: "Индекс НКИ",
                    _type: "string",
                        _default: "",
                            label: "Индекс НКИ"
        },
        DivisionDOR: { // Отдел ДОР ..........................................
            _mtype: "prop",
                _header: "Отдел ДОР",
                    _type: "string",
                        _default: "",
                            label: "Отдел ДОР"
        },
        UnPublishTranslatNumber: { // Номер неопубликованного перевода .......
            _mtype: "prop",
                _header: "Номер неопубликованного перевода",
                    _type: "string",
                        _default: "",
                            label: "Номер неопубликованного перевода"
        },
        AddressOrganNIR: { // Адрес организации НИР ..........................
            _mtype: "prop",
                _header: "Адрес организации НИР",
                    _type: "string",
                        _default: "",
                            label: "Адрес организации НИР"
        },
        VolumeNumber: { // Номер тома ........................................
            _mtype: "prop",
                _header: "Номер тома",
                    _type: "string",
                        _default: "",
                            label: "Номер тома"
        },
        DateRegistrationsDocum: { // Дата регистрации документа ..............
            _mtype: "prop",
                _header: "Дата регистрации документа",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата регистрации документа"
        },
        DateresentingDemand: { // Дата подачи заявки изобретения .............
            _mtype: "prop",
                _header: "Дата подачи заявки изобретения",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата подачи заявки изобретения"
        },
        InformationReferingTitle: { // Сведения, относящиеся к заглавию ......
            _mtype: "prop",
                _header: "Сведения, относящиеся к заглавию",
                    _type: "string",
                        _default: "",
                            label: "Сведения, относящиеся к заглавию"
        },
        DateStatementNTD: { // Дата утверждения НТД ..........................
            _mtype: "prop",
                _header: "Дата утверждения НТД",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата утверждения НТД"
        },
        CodeOCPOOrganDevelop: { // Код ОКПО организации-разработчика .........
            _mtype: "prop",
                _header: "Код ОКПО организации-разработчика",
                    _type: "string",
                        _default: "",
                            label: "Код ОКПО организации-разработчика"
        },
        PatentNumber: { // Номер патента .....................................
            _mtype: "prop",
                _header: "Номер патента",
                    _type: "string",
                        _default: "",
                            label: "Номер патента"
        },
        LeaderOrgan: { // Руководитель организации ...........................
            _mtype: "prop",
                _header: "Руководитель организации",
                    _type: "string",
                        _default: "",
                            label: "Руководитель организации"
        },
        IndividualPatentHolder: { // Индивидуальный заявитель изобретения ....
            _mtype: "prop",
                _header: "Индивидуальный заявитель изобретения",
                    _type: "string",
                        _default: "",
                            label: "Индивидуальный заявитель изобретения"
        },
        OrganKeeperDocFirsthand: { // Организация хранитель документа (первоисточника)
            _mtype: "prop",
                _header: "Организация хранитель документа (первоисточника)",
                    _type: "string",
                        _default: "",
                            label: "Организация хранитель документа (первоисточника)"
        },
        DateIntroductionNTD: { // Дата введения НТД в действие ...............
            _mtype: "prop",
                _header: "Дата введения НТД в действие",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата введения НТД в действие"
        },
        CipherKeepingDocument: { // Шифр хранения документа в организации хранителя документа
            _mtype: "prop",
                _header: "Шифр хранения документа в организации хранителя документа",
                    _type: "string",
                        _default: "",
                            label: "Шифр хранения документа в организации хранителя документа"
        },
        ShortNameOrganNIR: { // Сокращенное наименование организации исполнителя НИР
            _mtype: "prop",
                _header: "Сокращенное наименование организации исполнителя НИР",
                    _type: "string",
                        _default: "",
                            label: "Сокращенное наименование организации исполнителя НИР"
        },
        FirsthandName: { // Название первоисточника ..........................
            _mtype: "prop",
                _header: "Название первоисточника",
                    _type: "string",
                        _default: "",
                            label: "Название первоисточника"
        },
        DateDepositions: { // Дата депонирования .............................
            _mtype: "prop",
                _header: "Дата депонирования",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата депонирования"
        },
        ScientificDegreeLeader: { // Ученая степень руководителя .............
            _mtype: "prop",
                _header: "Ученая степень руководителя",
                    _type: "string",
                        _default: "",
                            label: "Ученая степень руководителя"
        },
        Translator: { // Переводчик ..........................................
            _mtype: "prop",
                _header: "Переводчик",
                    _type: "string",
                        _default: "",
                            label: "Переводчик"
        },
        FaxOrganNIR: { // Факс организации исполнителя НИР ...................
            _mtype: "prop",
                _header: "Факс организации исполнителя НИР",
                    _type: "string",
                        _default: "",
                            label: "Факс организации исполнителя НИР"
        },
        NumberGosRegistrations: { // Номер госрегистрации ....................
            _mtype: "prop",
                _header: "Номер госрегистрации",
                    _type: "string",
                        _default: "",
                            label: "Номер госрегистрации"
        },
        DateProtectionThesises: { // Дата защиты диссертации .................
            _mtype: "prop",
                _header: "Дата защиты диссертации",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата защиты диссертации"
        },
        IndexMPC: { // Индекс МПК (МКИ) ......................................
            _mtype: "prop",
                _header: "Индекс МПК (МКИ)",
                    _type: "string",
                        _default: "",
                            label: "Индекс МПК (МКИ)"
        },
        IndicationNTD: { // Обозначение НТД ..................................
            _mtype: "prop",
                _header: "Обозначение НТД",
                    _type: "string",
                        _default: "",
                            label: "Обозначение НТД"
        },
        DateBeginningWork: { // Дата начала работ по теме ....................
            _mtype: "prop",
                _header: "Дата начала работ по теме",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата начала работ по теме"
        },
        SiteOrganDepositor: { // Местонахождение организации депонента .......
            _mtype: "prop",
                _header: "Местонахождение организации депонента",
                    _type: "string",
                        _default: "",
                            label: "Местонахождение организации депонента"
        },
        DatePublishingDemand: { // Дата опубликования заявки изобретения .....
            _mtype: "prop",
                _header: "Дата опубликования заявки изобретения",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата опубликования заявки изобретения"
        },
        IssueNumber: { // Номер выпуска сериального издания ..................
            _mtype: "prop",
                _header: "Номер выпуска сериального издания",
                    _type: "string",
                        _default: "",
                            label: "Номер выпуска сериального издания"
        },
        TranslationName: { // Перевод основного названия на русский язык .....
            _mtype: "prop",
                _header: "Перевод основного названия на русский язык",
                    _type: "string",
                        _default: "",
                            label: "Перевод основного названия на русский язык"
        },
        Publishers: { // Издательство ........................................
            _mtype: "prop",
                _header: "Издательство",
                    _type: "string",
                        _default: "",
                            label: "Издательство"
        },
        OrganPerformerNIR: { // Организация-исполнитель НИР ..................
            _mtype: "prop",
                _header: "Организация-исполнитель НИР",
                    _type: "string",
                        _default: "",
                            label: "Организация-исполнитель НИР"
        },
        DateCompletionsWork: { // Дата окончания работ по теме ...............
            _mtype: "prop",
                _header: "Дата окончания работ по теме",
                    _type: "DateTime",
                        _default: "",
                            label: "Дата окончания работ по теме"
        },
        ReflectionAbstractService: { // Отражение реферативными службами .....
            _mtype: "prop",
                _header: "Отражение реферативными службами",
                    _type: "string",
                        _default: "",
                            label: "Отражение реферативными службами"
        }
    },
    MCountry: { // Страна ----------------------------------------------------
        _mtype: "class",
            _header: "Страна",
                _base: "MEDMObj",
                    _table: "Country",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Latin: { // Код ......................................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        Numerical: { // Код 1 ................................................
            _mtype: "prop",
                _header: "Код 1",
                    _type: "string",
                        _default: "",
                            label: "Код 1"
        },
        Russian2: { // Код 2 .................................................
            _mtype: "prop",
                _header: "Код 2",
                    _type: "string",
                        _default: "",
                            label: "Код 2"
        },
        Russian3: { // Код 3 .................................................
            _mtype: "prop",
                _header: "Код 3",
                    _type: "string",
                        _default: "",
                            label: "Код 3"
        },
        CountryName: { // Наименование .......................................
            _mtype: "prop",
                _header: "Наименование",
                    _type: "string",
                        _default: "",
                            label: "Наименование",
                                column: {
                fillspace: 1
            }
        }
    },
    MLanguage: { // Язык -----------------------------------------------------
        _mtype: "class",
            _header: "Язык",
                _base: "MEDMObj",
                    _table: "Language",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Latin: { // Код ......................................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        Numerical: { // Код 1 ................................................
            _mtype: "prop",
                _header: "Код 1",
                    _type: "string",
                        _default: "",
                            label: "Код 1"
        },
        Russian2: { // Код 2 .................................................
            _mtype: "prop",
                _header: "Код 2",
                    _type: "string",
                        _default: "",
                            label: "Код 2"
        },
        Russian3: { // Код 3 .................................................
            _mtype: "prop",
                _header: "Код 3",
                    _type: "string",
                        _default: "",
                            label: "Код 3"
        },
        LanguageName: { // Наименование ......................................
            _mtype: "prop",
                _header: "Наименование",
                    _type: "string",
                        _default: "",
                            label: "Наименование",
                                column: {
                fillspace: 1
            }
        }
    },
    MTypeJob: { // Тип работы ------------------------------------------------
        _mtype: "class",
            _header: "Тип работы",
                _base: "MEDMObj",
                    _table: "TypeJob",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Code: { // Код .......................................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        Job: { // Наименование ...............................................
            _mtype: "prop",
                _header: "Наименование",
                    _type: "string",
                        _default: "",
                            label: "Наименование",
                                column: {
                fillspace: 1
            }
        }
    },
    MTypePatent: { // Тип патента --------------------------------------------
        _mtype: "class",
            _header: "Тип патента",
                _base: "MEDMObj",
                    _table: "TypePatent",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Type: { // Код .......................................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        Name: { // Наименование ..............................................
            _mtype: "prop",
                _header: "Наименование",
                    _type: "string",
                        _default: "",
                            label: "Наименование",
                                column: {
                fillspace: 1
            }
        }
    },
    MDocChar: { // Вид документа ---------------------------------------------
        _mtype: "class",
            _header: "Вид документа",
                _base: "MEDMObj",
                    _table: "DocChar",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Code: { // Код .......................................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        DocCharacter: { // Наименование ......................................
            _mtype: "prop",
                _header: "Наименование",
                    _type: "string",
                        _default: "",
                            label: "Наименование",
                                column: {
                fillspace: 1
            }
        }
    },
    MRubricator: { // Рубрикатор ---------------------------------------------
        _mtype: "class",
            _header: "Рубрикатор",
                _base: "MEDMObj",
                    _table: "Rubricator",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Name: { // Код .......................................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        Caption: { // Наименование ...........................................
            _mtype: "prop",
                _header: "Наименование",
                    _type: "string",
                        _default: "",
                            label: "Наименование",
                                column: {
                fillspace: 1
            }
        },
        webix_kids: {
            get: function () {
                return true;
            },
            set: function (value) {
            }
        }
    },
    MDoc2KeyWord: { // Doc2KeyWord -------------------------------------------
        _mtype: "class",
            _header: "Doc2KeyWord",
                _base: "MEDMObj",
                    _table: "Doc2KeyWord",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        KeyWord: { // Ключевое слово .........................................
            _mtype: "prop",
                _type: "MKeyWord",
                    _default: { },
            label: "Ключевое слово"
        },
        Doc: { // Документ ...................................................
            _mtype: "prop",
                _type: "MDoc",
                    _default: { },
            label: "Документ"
        }
    },
    MDoc2Descriptors: { // Doc2Descriptors -----------------------------------
        _mtype: "class",
            _header: "Doc2Descriptors",
                _base: "MEDMObj",
                    _table: "Doc2Descriptors",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Descriptors: { // Дескриптор .........................................
            _mtype: "prop",
                _type: "MDescriptors",
                    _default: { },
            _rid: "DescriptorsId",
                label: "Дескриптор"
        },
        DocId: { // Документ .................................................
            _mtype: "prop",
                _header: "Документ",
                    _type: "Guid",
                        _default: "",
                            _visible: false
        },
        Doc: { // Документ ...................................................
            _mtype: "prop",
                _type: "MDoc",
                    _default: { },
            _rid: "DocId",
                label: "Документ"
        }
    },
    MDoc2Rubricator: { // Doc2Rubricator -------------------------------------
        _mtype: "class",
            _header: "Doc2Rubricator",
                _base: "MEDMObj",
                    _table: "Doc2Rubricator",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Rubricator: { // Рубрикатор ..........................................
            _mtype: "prop",
                _type: "MRubricator",
                    _default: { },
            label: "Рубрикатор"
        },
        Doc: { // Документ ...................................................
            _mtype: "prop",
                _type: "MDoc",
                    _default: { },
            label: "Документ"
        }
    },
    MKeyWord: { // Ключевое слово --------------------------------------------
        _mtype: "class",
            _header: "Ключевое слово",
                _base: "MEDMObj",
                    _table: "KeyWord",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        KeyWord: { // Ключевое слово .........................................
            _mtype: "prop",
                _header: "Ключевое слово",
                    _type: "string",
                        _default: "",
                            label: "Ключевое слово",
                                column: {
                fillspace: 1
            }
        }
    },
    MDescriptors: { // Дескриптор --------------------------------------------
        _mtype: "class",
            _header: "Дескриптор",
                _base: "MEDMObj",
                    _table: "Descriptors",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        CodeDescript: { // Код ...............................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        Descript: { // Наименование ..........................................
            _mtype: "prop",
                _header: "Наименование",
                    _type: "string",
                        _default: "",
                            label: "Наименование",
                                column: {
                fillspace: 1
            }
        }
    },
    MLiderSign: { // Певый символ --------------------------------------------
        _mtype: "class",
            _header: "Певый символ",
                _base: "MObj",
                    _table: "LiderSign",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "string",
                        _default: "",
                            _visible: false,
                                label: "Id"
        }
    },
    MStat: { // Статистика ---------------------------------------------------
        _mtype: "class",
            _header: "Статистика",
                _base: "MObj",
                    _table: "Stat",
                        Count: { // Кол-во ...................................................
            _mtype: "prop",
                _header: "Кол-во",
                    _type: "int",
                        _default: "",
                            label: "Кол-во"
        },
        DocType: { // Тип документа ..........................................
            _mtype: "prop",
                _type: "MDocType",
                    _default: { },
            label: "Тип документа"
        }
    },
    MStatField: { // Заполняемость полей -------------------------------------
        _mtype: "class",
            _header: "Заполняемость полей",
                _base: "MObj",
                    _table: "StatField",
                        Count: { // Кол-во ...................................................
            _mtype: "prop",
                _header: "Кол-во",
                    _type: "int",
                        _default: "",
                            label: "Кол-во"
        },
        DocGroup: { // Тип документа .........................................
            _mtype: "prop",
                _type: "MDocGroup",
                    _default: { },
            label: "Тип документа"
        },
        FieldName: { // Наименование поля ....................................
            _mtype: "prop",
                _header: "Наименование поля",
                    _type: "string",
                        _default: "",
                            label: "Наименование поля"
        }
    },
    MFindFilter: { // Фильтр поиска ------------------------------------------
        _mtype: "class",
            _header: "Фильтр поиска",
                _base: "MEDMObj",
                    _table: "FindFilter",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "string",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        DocTypeList: { // Тип документа ......................................
            _mtype: "prop",
                _header: "Тип документа",
                    _type: "string",
                        _default: "",
                            label: "Тип документа"
        },
        Name: { // Искать по .................................................
            _mtype: "prop",
                _header: "Искать по",
                    _type: "string",
                        _default: "",
                            label: "Искать по"
        },
        FindInName: { // в наименовании ......................................
            _mtype: "prop",
                _header: "в наименовании",
                    _type: "bool",
                        _default: true,
                            label: "в наименовании"
        },
        FindInAbstract: { // в реферате ......................................
            _mtype: "prop",
                _header: "в реферате",
                    _type: "bool",
                        _default: true,
                            label: "в реферате"
        },
        FindInRubric: { // в рубрикаторе .....................................
            _mtype: "prop",
                _header: "в рубрикаторе",
                    _type: "bool",
                        _default: false,
                            label: "в рубрикаторе"
        },
        FindInDescript: { // в дескрипторе ...................................
            _mtype: "prop",
                _header: "в дескрипторе",
                    _type: "bool",
                        _default: false,
                            label: "в дескрипторе"
        },
        FindInKeyWord: { // в ключевых словах ................................
            _mtype: "prop",
                _header: "в ключевых словах",
                    _type: "bool",
                        _default: false,
                            label: "в ключевых словах"
        },
        CipherSpreading: { // Шифр распространения ...........................
            _mtype: "prop",
                _header: "Шифр распространения",
                    _type: "string",
                        _default: "",
                            label: "Шифр распространения"
        },
        DateRecordSystem: { // Дата ввода записи .............................
            _mtype: "prop",
                _header: "Дата ввода записи",
                    _type: "DateTimeInterval",
                        _default: "",
                            label: "Дата ввода записи"
        },
        DatePublishing: { // Дата издания ....................................
            _mtype: "prop",
                _header: "Дата издания",
                    _type: "DateTimeInterval",
                        _default: "",
                            label: "Дата издания"
        },
        FirsthandName: { // Название первоисточника ..........................
            _mtype: "prop",
                _header: "Название первоисточника",
                    _type: "string",
                        _default: "",
                            label: "Название первоисточника"
        },
        IndexUDC: { // Индекс УДК ............................................
            _mtype: "prop",
                _header: "Индекс УДК",
                    _type: "string",
                        _default: "",
                            label: "Индекс УДК"
        },
        IndividualAuthor: { // Автор .........................................
            _mtype: "prop",
                _header: "Автор",
                    _type: "string",
                        _default: "",
                            label: "Автор"
        },
        TranslationName: { // Перевод основного названия на русский язык .....
            _mtype: "prop",
                _header: "Перевод основного названия на русский язык",
                    _type: "string",
                        _default: "",
                            label: "Перевод основного названия на русский язык"
        },
        Publishers: { // Издательство ........................................
            _mtype: "prop",
                _header: "Издательство",
                    _type: "string",
                        _default: "",
                            label: "Издательство"
        },
        PlacePublishingCountryList: { // Страна издания ......................
            _mtype: "prop",
                _header: "Страна издания",
                    _type: "string",
                        _default: "",
                            label: "Страна издания"
        },
        LanguageMainTextList: { // Язык публикации ...........................
            _mtype: "prop",
                _header: "Язык публикации",
                    _type: "string",
                        _default: "",
                            label: "Язык публикации"
        },
        DescriptorsList: { // Дескриптор .....................................
            _mtype: "prop",
                _header: "Дескриптор",
                    _type: "string",
                        _default: "",
                            label: "Дескриптор"
        },
        RubricatorList: { // Рубрикатор ......................................
            _mtype: "prop",
                _header: "Рубрикатор",
                    _type: "string",
                        _default: "",
                            label: "Рубрикатор"
        },
        KeyWordList: { // Ключевое слово .....................................
            _mtype: "prop",
                _header: "Ключевое слово",
                    _type: "string",
                        _default: "",
                            label: "Ключевое слово"
        },
        SortMode: { // Сортировка ............................................
            _mtype: "prop",
                _header: "Сортировка",
                    _type: "int",
                        _default: 1,
                            label: "Сортировка"
        }
    },
    MUserMark: { // Помеченные объекты ---------------------------------------
        _mtype: "class",
            _header: "Помеченные объекты",
                _base: "MEDMObj",
                    _table: "UserMark",
                        Id: { // Id  .........................................................
            _mtype: "prop",
                _header: "Id ",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id "
        },
        UserId: { // Пользователь ............................................
            _mtype: "prop",
                _header: "Пользователь",
                    _type: "Guid",
                        _default: "",
                            label: "Пользователь"
        },
        ObjId: { // Id объекта ...............................................
            _mtype: "prop",
                _header: "Id объекта",
                    _type: "Guid",
                        _default: "",
                            label: "Id объекта"
        },
        ObjType: { // Тип объекта ............................................
            _mtype: "prop",
                _header: "Тип объекта",
                    _type: "string",
                        _default: "",
                            label: "Тип объекта"
        }
    },
    MUser: { // User ---------------------------------------------------------
        _mtype: "class",
            _header: "User",
                _base: "MEDMObj",
                    _table: "User",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        UserGroup: { // UserGroupId ..........................................
            _mtype: "prop",
                _type: "MUserGroup",
                    _default: { },
            label: "UserGroupId"
        },
        Code: { // Код .......................................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        Name: { // Фио .......................................................
            _mtype: "prop",
                _header: "Фио",
                    _type: "string",
                        _default: "",
                            label: "Фио"
        },
        Phone: { // Телефон ..................................................
            _mtype: "prop",
                _header: "Телефон",
                    _type: "string",
                        _default: "",
                            label: "Телефон"
        },
        EMail: { // Почта ....................................................
            _mtype: "prop",
                _header: "Почта",
                    _type: "string",
                        _default: "",
                            label: "Почта"
        },
        IsAdmin: { // Админ ..................................................
            _mtype: "prop",
                _header: "Админ",
                    _type: "bool",
                        _default: false,
                            label: "Админ"
        },
        IsEditor: { // Правка ................................................
            _mtype: "prop",
                _header: "Правка",
                    _type: "bool",
                        _default: false,
                            label: "Правка"
        },
        PassCode: { // Пароль ................................................
            _mtype: "prop",
                _header: "Пароль",
                    _type: "long",
                        _default: "",
                            label: "Пароль"
        }
    },
    MUserGroup: { // UserGroup -----------------------------------------------
        _mtype: "class",
            _header: "UserGroup",
                _base: "MEDMObj",
                    _table: "UserGroup",
                        Id: { // Id ..........................................................
            _mtype: "prop",
                _header: "Id",
                    _type: "Guid",
                        _default: "",
                            _visible: false,
                                label: "Id"
        },
        Code: { // Код .......................................................
            _mtype: "prop",
                _header: "Код",
                    _type: "string",
                        _default: "",
                            label: "Код"
        },
        Name: { // Наименование ..............................................
            _mtype: "prop",
                _header: "Наименование",
                    _type: "string",
                        _default: "",
                            label: "Наименование"
        }
    }
}
