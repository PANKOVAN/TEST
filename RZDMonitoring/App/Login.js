( {
  "view": "layout",
  "cols": [
    {
      "rows": [
        {
        },
        {
          "cols": [
            {},
            {
              "view": "form",
              "id": "log_form",
              "width": 350,
              "borderless": true,
              "margin": 5,
              "elements": [
                {
                  "view": "label",
                  "label": "<h2 class=\"text-primary\" >Вход</h2>",
                  "height": 80
                },
                {
                  "view": "text",
                  "label": "Логин",
                  "name": "login"
                },
                {
                  "view": "text",
                  "type": "password",
                  "label": "Пароль",
                  "name": "password"
                },
                {
                  "view": "button",
                  "value": "OK",
                  "type": "form",
                  "width": 100,
                  "align": "right",
                  "click": "m.main.login('log_form')"
                }
              ]

            },
            {
            }
          ]
        },
        {
        }
      ]
    },
    {
      "css": "login-bg",
      "rows": [
        {},
        {
          "borderless": true,
          "view": "template",
            "template": "<h1>Автоматизированная система поиска, обработки и хранения информаци по основным направлениям деятельности ОАО РЖД<h1>",
            "autoheight": true
        },
        {}
      ]
    }
  ]
})
