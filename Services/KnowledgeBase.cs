using OSExpertSystemWinUI.Models;

namespace OSExpertSystemWinUI.Services;

public sealed class KnowledgeBase
{
    public IReadOnlyList<ChoiceItem> DeviceTypes { get; } =
    [
        new("home_laptop", "Домашній ноутбук", "Навчання, офіс, браузер, відеозв'язок"),
        new("office_pc", "Офісний ПК", "Робота з документами, поштою та корпоративними сервісами"),
        new("weak_pc", "Слабкий або старий ПК", "Обмежена оперативна пам'ять і невисока продуктивність"),
        new("developer_workstation", "Робоча станція розробника", "IDE, Docker, компілятори, термінал, Git"),
        new("server", "Сервер", "Файловий, веб-, БД або інфраструктурний сервер"),
        new("gaming_pc", "Ігровий ПК", "Ігри, драйвери GPU, Steam/Epic/Battle.net"),
        new("apple_device", "Пристрій Apple", "MacBook, iMac або користувач в екосистемі Apple")
    ];

    public IReadOnlyList<ChoiceItem> MainTasks { get; } =
    [
        new("office_study", "Офіс і навчання", "Документи, презентації, пошта, браузер"),
        new("programming", "Програмування", "IDE, Git, Docker, Linux tooling, компілятори"),
        new("design_media", "Дизайн і медіа", "Монтаж, графіка, фото, творчі програми"),
        new("gaming", "Ігри", "Максимальна сумісність з іграми та драйверами"),
        new("server_admin", "Серверні задачі", "Стабільна робота сервісів у мережі"),
        new("privacy_security", "Приватність і безпека", "Контроль системи, менше телеметрії, прозорість"),
        new("web_only", "Переважно веб-сервіси", "Браузер, хмарні документи, мінімальне адміністрування")
    ];

    public IReadOnlyList<ChoiceItem> HardwareLevels { get; } =
    [
        new("weak", "Слабке залізо", "До 4-8 ГБ RAM, старий CPU або HDD"),
        new("medium", "Середній ПК", "8-16 ГБ RAM, SSD, типовий сучасний процесор"),
        new("powerful", "Потужна система", "16+ ГБ RAM, продуктивний CPU/GPU, SSD/NVMe")
    ];

    public IReadOnlyList<ChoiceItem> UserExperience { get; } =
    [
        new("beginner", "Початківець", "Потрібна простота й мінімум налаштувань"),
        new("regular", "Звичайний користувач", "Може виконувати базове налаштування"),
        new("advanced", "Досвідчений користувач", "Готовий працювати з налаштуваннями й терміналом"),
        new("admin", "Адміністратор", "Може супроводжувати систему в мережі")
    ];

    public IReadOnlyList<ChoiceItem> Compatibility { get; } =
    [
        new("windows_apps", "Windows-програми", "MS Office, 1С/облік, драйвери, корпоративне ПЗ"),
        new("open_source", "Open-source інструменти", "Linux tooling, Python, GCC, Docker, серверні пакети"),
        new("apple_ecosystem", "Apple-екосистема", "iPhone/iCloud/AirDrop/Final Cut/Logic"),
        new("browser_cloud", "Браузер і хмара", "Google Docs, Microsoft 365 Web, CRM у браузері"),
        new("mixed", "Змішане середовище", "Потрібна універсальність і підтримка різного ПЗ")
    ];

    public IReadOnlyList<ChoiceItem> SecurityPriorities { get; } =
    [
        new("standard", "Стандартна безпека", "Автооновлення, базовий захист, типові ризики"),
        new("high", "Підвищена безпека", "Стабільність, контроль прав, довготривала підтримка"),
        new("privacy", "Приватність", "Менше телеметрії, прозорість, контроль системи")
    ];

    public IReadOnlyList<ChoiceItem> Administration { get; } =
    [
        new("simple", "Просте обслуговування", "Користувач самостійно обслуговує систему"),
        new("manual", "Ручне адміністрування", "Адміністратор або досвідчений користувач"),
        new("centralized", "Централізоване керування", "Домен, групові політики, масове розгортання")
    ];

    public IReadOnlyList<ChoiceItem> Budgets { get; } =
    [
        new("free_or_existing", "Безкоштовно або вже є ліцензія", "Перевага безкоштовним або наявним рішенням"),
        new("paid_ok", "Платна ліцензія допустима", "Готовність придбати ОС або пристрій"),
        new("enterprise", "Корпоративна ліцензія", "Організація має або планує централізовані ліцензії")
    ];

    public IReadOnlyList<OsOption> Options { get; } =
    [
        new(
            "windows11",
            "Windows 11",
            "Універсальна ОС для більшості сучасних ПК, офісу, навчання, ігор і Windows-сумісного ПЗ.",
            "Рекомендовано Windows 11, якщо потрібна максимальна сумісність із популярними програмами, драйверами, офісним ПЗ, іграми та звичним інтерфейсом. Найкраще підходить для середнього або потужного обладнання."),
        new(
            "windows_enterprise",
            "Windows 11 Pro / Enterprise",
            "Варіант для організацій із централізованим адмініструванням, доменом і корпоративними політиками.",
            "Рекомендовано Windows Pro/Enterprise, якщо комп'ютери входять у домен, потрібні групові політики, BitLocker, централізоване керування, корпоративне ПЗ і контроль доступу."),
        new(
            "ubuntu",
            "Ubuntu LTS",
            "Стабільна Linux-система для розробки, навчання, серверних задач і безкоштовного використання.",
            "Рекомендовано Ubuntu LTS, якщо потрібна безкоштовна система з довготривалою підтримкою, Linux-інструментами, зручним встановленням і широкою спільнотою."),
        new(
            "linux_mint",
            "Linux Mint",
            "Дружня Linux-система для слабших ПК і користувачів, які переходять із Windows.",
            "Рекомендовано Linux Mint для старіших або слабших комп'ютерів, а також для користувачів, яким потрібна проста безкоштовна система з інтерфейсом, схожим на класичний desktop."),
        new(
            "debian_server",
            "Debian Server",
            "Надійна серверна ОС для стабільної роботи мережевих служб.",
            "Рекомендовано Debian Server для серверів, де важливі стабільність, контроль пакетів, мінімалізм, безпека та довготривала підтримка без зайвих компонентів."),
        new(
            "macos",
            "macOS",
            "ОС для пристроїв Apple, творчих задач, дизайну та інтеграції з екосистемою Apple.",
            "Рекомендовано macOS, якщо користувач працює на Mac, використовує iCloud, AirDrop, iPhone, Final Cut, Logic або інші Apple-сервіси. Сильний варіант для дизайну, медіа та мобільної розробки під iOS."),
        new(
            "chromeos",
            "ChromeOS",
            "Легка система для браузера, навчання, хмарних сервісів і мінімального адміністрування.",
            "Рекомендовано ChromeOS, якщо основні задачі виконуються в браузері, потрібна простота, швидкий старт, автоматичні оновлення та мінімальне обслуговування.")
    ];

    public IReadOnlyList<ExpertRule> Rules { get; } =
    [
        Rule("R1", "windows11", "Сумісність із Windows-програмами", 32, new() { ["SoftwareCompatibility"] = "windows_apps" }, "Експерт враховує, що більшість комерційних і корпоративних програм найкраще підтримуються у Windows."),
        Rule("R2", "windows11", "Ігровий сценарій", 28, new() { ["MainTask"] = "gaming" }, "Для ігор важлива підтримка DirectX, драйверів відеокарт і популярних ігрових платформ."),
        Rule("R3", "windows11", "Сучасний домашній ноутбук", 18, new() { ["DeviceType"] = "home_laptop", ["HardwareLevel"] = "medium" }, "Для типового сучасного ноутбука Windows 11 забезпечує простий старт і достатню сумісність."),
        Rule("R4", "windows_enterprise", "Централізоване адміністрування", 34, new() { ["Administration"] = "centralized" }, "В організаціях експерт надає перевагу ОС із підтримкою домену, політик, шифрування та централізованого керування."),
        Rule("R5", "windows_enterprise", "Корпоративна ліцензія", 22, new() { ["Budget"] = "enterprise" }, "Наявність корпоративної ліцензії робить Windows Pro/Enterprise раціональним вибором для мережі організації."),
        Rule("R6", "windows_enterprise", "Офісний ПК із Windows-ПЗ", 24, new() { ["DeviceType"] = "office_pc", ["SoftwareCompatibility"] = "windows_apps" }, "Для офісу часто критична робота бухгалтерського, документообігового та корпоративного ПЗ."),
        Rule("R7", "ubuntu", "Розробка та open-source інструменти", 30, new() { ["MainTask"] = "programming", ["SoftwareCompatibility"] = "open_source" }, "Для програмування експерт враховує доступність компіляторів, пакетних менеджерів, Docker та Linux tooling."),
        Rule("R8", "ubuntu", "Безкоштовність і універсальність", 18, new() { ["Budget"] = "free_or_existing", ["UserExperience"] = "advanced" }, "Досвідчений користувач може ефективно працювати з Ubuntu без купівлі ліцензії."),
        Rule("R9", "ubuntu", "Приватність і контроль", 20, new() { ["SecurityPriority"] = "privacy", ["UserExperience"] = "advanced" }, "Linux дозволяє краще контролювати склад системи, сервіси та телеметрію."),
        Rule("R10", "linux_mint", "Слабкий ПК", 34, new() { ["HardwareLevel"] = "weak" }, "На слабкому або старому обладнанні експерт обирає легшу ОС, щоб зменшити навантаження."),
        Rule("R11", "linux_mint", "Початківець і безкоштовність", 20, new() { ["UserExperience"] = "beginner", ["Budget"] = "free_or_existing" }, "Linux Mint має простіший desktop-інтерфейс і підходить для базових задач без оплати ліцензії."),
        Rule("R12", "linux_mint", "Офіс і навчання на слабкому ПК", 22, new() { ["MainTask"] = "office_study", ["HardwareLevel"] = "weak" }, "Для навчання й офісу на слабкому комп'ютері важлива легкість системи."),
        Rule("R13", "debian_server", "Серверний пристрій", 38, new() { ["DeviceType"] = "server" }, "Для сервера експерт оцінює стабільність, мінімалізм, контроль оновлень і надійність."),
        Rule("R14", "debian_server", "Серверні задачі", 32, new() { ["MainTask"] = "server_admin" }, "Для мережевих сервісів потрібна стабільна серверна ОС із довготривалою підтримкою."),
        Rule("R15", "debian_server", "Ручне адміністрування", 18, new() { ["Administration"] = "manual", ["UserExperience"] = "admin" }, "Адміністратор може ефективно супроводжувати Debian, налаштовувати служби та політики без зайвого GUI."),
        Rule("R16", "macos", "Пристрій Apple", 40, new() { ["DeviceType"] = "apple_device" }, "На пристроях Apple експерт обирає macOS через сумісність заліза, оновлень і сервісів."),
        Rule("R17", "macos", "Дизайн і медіа", 26, new() { ["MainTask"] = "design_media", ["SoftwareCompatibility"] = "apple_ecosystem" }, "Для творчих задач macOS має сильну екосистему професійних інструментів і стабільну роботу на Apple hardware."),
        Rule("R18", "macos", "Платна ліцензія допустима", 12, new() { ["Budget"] = "paid_ok", ["SoftwareCompatibility"] = "apple_ecosystem" }, "Якщо користувач готовий купити Apple-пристрій, macOS є логічним вибором."),
        Rule("R19", "chromeos", "Переважно браузерні задачі", 34, new() { ["MainTask"] = "web_only" }, "Якщо більшість роботи виконується у браузері, експерт обирає просту систему з мінімальним адмініструванням."),
        Rule("R20", "chromeos", "Хмарна сумісність", 24, new() { ["SoftwareCompatibility"] = "browser_cloud", ["Administration"] = "simple" }, "Для хмарних сервісів ChromeOS забезпечує швидкий старт, автооновлення та просте обслуговування."),
        Rule("R21", "chromeos", "Початківець", 14, new() { ["UserExperience"] = "beginner", ["MainTask"] = "web_only" }, "Початківцю простіше працювати з системою, де більшість задач зосереджена в браузері.")
    ];

    private static ExpertRule Rule(string id, string optionId, string title, int weight, Dictionary<string, string> conditions, string explanation)
        => new(id, optionId, title, weight, conditions, explanation);
}
