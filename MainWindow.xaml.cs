using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using OSExpertSystemWinUI.Models;
using OSExpertSystemWinUI.Services;
using OSExpertSystemWinUI.UI;

namespace OSExpertSystemWinUI;

public sealed partial class MainWindow : Window
{
    private readonly KnowledgeBase _kb = new();
    private readonly ExpertEngine _engine;
    private UserProfile _profile = new();
    private IReadOnlyList<Recommendation> _lastRecommendations = [];
    private bool _isCompactLayout;

    public MainWindow()
    {
        InitializeComponent();
        WindowSetupService.Configure(this, "OS Selection Expert", "Assets/AppIcon.ico");
        SizeChanged += OnWindowSizeChanged;

        _engine = new ExpertEngine(_kb);
        InitializeInputs();
        UpdateProfileFromUi();
        EvaluateAndRender();
    }

    private void InitializeInputs()
    {
        DeviceTypeBox.ItemsSource = _kb.DeviceTypes;
        MainTaskBox.ItemsSource = _kb.MainTasks;
        HardwareBox.ItemsSource = _kb.HardwareLevels;
        ExperienceBox.ItemsSource = _kb.UserExperience;
        CompatibilityBox.ItemsSource = _kb.Compatibility;
        SecurityBox.ItemsSource = _kb.SecurityPriorities;
        AdministrationBox.ItemsSource = _kb.Administration;
        BudgetBox.ItemsSource = _kb.Budgets;

        DeviceTypeBox.SelectedIndex = 0;
        MainTaskBox.SelectedIndex = 0;
        HardwareBox.SelectedIndex = 1;
        ExperienceBox.SelectedIndex = 1;
        CompatibilityBox.SelectedIndex = 0;
        SecurityBox.SelectedIndex = 0;
        AdministrationBox.SelectedIndex = 0;
        BudgetBox.SelectedIndex = 0;
    }

    private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs args)
    {
        ApplyResponsiveLayout(args.Size.Width < 1120);
    }

    private void ApplyResponsiveLayout(bool compact)
    {
        if (_isCompactLayout == compact) return;
        _isCompactLayout = compact;

        if (compact)
        {
            FormColumn.Width = new GridLength(1, GridUnitType.Star);
            ResultColumn.Width = new GridLength(0);
            Grid.SetColumn(ResultPane, 0);
            Grid.SetRow(ResultPane, 1);
            SummaryGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
            Grid.SetColumn(ScorePanel, 0);
            Grid.SetRow(ScorePanel, 1);
            if (SummaryGrid.RowDefinitions.Count == 0)
            {
                SummaryGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                SummaryGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }
            ScorePanel.Margin = new Thickness(0, 16, 0, 0);
        }
        else
        {
            FormColumn.Width = new GridLength(460);
            ResultColumn.Width = new GridLength(1, GridUnitType.Star);
            Grid.SetColumn(ResultPane, 1);
            Grid.SetRow(ResultPane, 0);
            if (SummaryGrid.RowDefinitions.Count > 0)
            {
                SummaryGrid.RowDefinitions.Clear();
            }
            SummaryGrid.ColumnDefinitions[1].Width = new GridLength(300);
            Grid.SetColumn(ScorePanel, 1);
            Grid.SetRow(ScorePanel, 0);
            ScorePanel.Margin = new Thickness(0);
        }
    }

    private void OnInputChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DeviceTypeBox is null) return;
        UpdateProfileFromUi();
    }

    private void UpdateProfileFromUi()
    {
        _profile = new UserProfile
        {
            DeviceType = SelectedId(DeviceTypeBox, _profile.DeviceType),
            MainTask = SelectedId(MainTaskBox, _profile.MainTask),
            HardwareLevel = SelectedId(HardwareBox, _profile.HardwareLevel),
            UserExperience = SelectedId(ExperienceBox, _profile.UserExperience),
            SoftwareCompatibility = SelectedId(CompatibilityBox, _profile.SoftwareCompatibility),
            SecurityPriority = SelectedId(SecurityBox, _profile.SecurityPriority),
            Administration = SelectedId(AdministrationBox, _profile.Administration),
            Budget = SelectedId(BudgetBox, _profile.Budget)
        };
    }

    private static string SelectedId(ComboBox box, string fallback)
        => box.SelectedItem is ChoiceItem item ? item.Id : fallback;

    private void Evaluate_Click(object sender, RoutedEventArgs e)
    {
        UpdateProfileFromUi();
        EvaluateAndRender();
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        InitializeInputs();
        UpdateProfileFromUi();
        EvaluateAndRender();
    }

    private void ShowConsultation_Click(object sender, RoutedEventArgs e)
    {
        ConsultationPanel.Visibility = Visibility.Visible;
        Grid.SetColumn(ResultPane, _isCompactLayout ? 0 : 1);
        Grid.SetColumnSpan(ResultPane, 1);
        ApplyResponsiveLayout(_isCompactLayout);
        RenderRecommendations();
    }

    private void ShowKnowledge_Click(object sender, RoutedEventArgs e)
    {
        ConsultationPanel.Visibility = Visibility.Collapsed;
        Grid.SetColumn(ResultPane, 0);
        Grid.SetColumnSpan(ResultPane, 2);
        RenderKnowledgeBase();
    }

    private void EvaluateAndRender()
    {
        _lastRecommendations = _engine.Evaluate(_profile);
        RenderRecommendations();
    }

    private void RenderRecommendations()
    {
        ContentHost.Children.Clear();

        var panel = new StackPanel { Spacing = 14 };
        ContentHost.Children.Add(panel);

        var best = _lastRecommendations.FirstOrDefault();
        if (best is not null)
        {
            BestTitleText.Text = best.Option.Name;
            BestDescriptionText.Text = best.Option.FullAdvice;
            BestPercentText.Text = best.PercentText;
            BestPercentLabelText.Text = "відповідності";
            BestProgress.Value = best.Percent;
        }

        panel.Children.Add(CreateSectionHeader("Рейтинг альтернатив", "Кожен варіант отримує бали за спрацьованими правилами бази знань."));

        foreach (var rec in _lastRecommendations)
        {
            panel.Children.Add(CreateRecommendationCard(rec));
        }
    }

    private UIElement CreateRecommendationCard(Recommendation rec)
    {
        var card = new Border
        {
            CornerRadius = new CornerRadius(24),
            Padding = new Thickness(20),
            Background = UiBrushes.Card,
            BorderBrush = UiBrushes.Brush(130, 255, 255, 255),
            BorderThickness = new Thickness(1)
        };

        var root = new Grid { ColumnSpacing = 16, RowSpacing = 12 };
        root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(210) });
        card.Child = root;

        var left = new StackPanel { Spacing = 8 };
        Grid.SetColumn(left, 0);
        root.Children.Add(left);

        left.Children.Add(new TextBlock
        {
            Text = rec.Option.Name,
            FontSize = 22,
            FontWeight = FontWeights.Bold,
            Foreground = UiBrushes.CardText
        });

        left.Children.Add(new TextBlock
        {
            Text = rec.Option.ShortDescription,
            TextWrapping = TextWrapping.Wrap,
            Foreground = UiBrushes.CardMuted
        });

        var details = new Expander
        {
            Header = $"Пояснення експерта: {rec.Hits.Count} правил",
            IsExpanded = rec.Score > 0 && rec.Percent >= 85
        };

        var hitPanel = new StackPanel { Spacing = 8, Margin = new Thickness(0, 8, 0, 0) };
        if (rec.Hits.Count == 0)
        {
            hitPanel.Children.Add(new TextBlock
            {
                Text = "Для цього рішення правила не спрацювали.",
                Foreground = UiBrushes.CardMuted
            });
        }
        else
        {
            foreach (var hit in rec.Hits)
            {
                hitPanel.Children.Add(new Border
                {
                    CornerRadius = new CornerRadius(14),
                    Padding = new Thickness(12),
                    Background = UiBrushes.RuleHitBackground,
                    Child = new TextBlock
                    {
                        Text = $"{hit.Title} (+{hit.Weight}): {hit.Explanation}",
                        TextWrapping = TextWrapping.Wrap,
                        Foreground = UiBrushes.CardText
                    }
                });
            }
        }

        details.Content = hitPanel;
        left.Children.Add(details);

        var right = new Border
        {
            CornerRadius = new CornerRadius(22),
            Background = UiBrushes.ScorePanel,
            BorderBrush = UiBrushes.ScoreBorder,
            BorderThickness = new Thickness(1),
            Padding = new Thickness(16),
            VerticalAlignment = VerticalAlignment.Stretch
        };
        Grid.SetColumn(right, 1);
        root.Children.Add(right);

        var scorePanel = new StackPanel { VerticalAlignment = VerticalAlignment.Center, Spacing = 8 };
        right.Child = scorePanel;
        scorePanel.Children.Add(new TextBlock
        {
            Text = rec.PercentText,
            Foreground = UiBrushes.ScoreText,
            HorizontalAlignment = HorizontalAlignment.Center,
            TextAlignment = TextAlignment.Center,
            FontWeight = FontWeights.Bold,
            FontSize = 32
        });
        scorePanel.Children.Add(new TextBlock
        {
            Text = "відповідності",
            Foreground = UiBrushes.ScoreMuted,
            HorizontalAlignment = HorizontalAlignment.Center,
            TextAlignment = TextAlignment.Center,
            FontSize = 12,
            FontWeight = FontWeights.SemiBold
        });
        scorePanel.Children.Add(new ProgressBar { Maximum = 100, Value = rec.Percent, Height = 8 });
        scorePanel.Children.Add(new TextBlock
        {
            Text = rec.ScoreText,
            Foreground = UiBrushes.ScoreMuted,
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 12
        });

        return card;
    }

    private void RenderKnowledgeBase()
    {
        ContentHost.Children.Clear();
        BestTitleText.Text = "База знань експертної системи";
        BestDescriptionText.Text = "Правила не є набором if-else у звіті, а подані як знання експерта: умови, вага та пояснення поради.";
        BestPercentText.Text = $"{_kb.Rules.Count}";
        BestPercentLabelText.Text = "правил у базі";
        BestProgress.Value = 100;

        var panel = new StackPanel { Spacing = 12 };
        ContentHost.Children.Add(panel);
        panel.Children.Add(CreateSectionHeader("Продукційні правила", "Кожне правило додає вагу певній ОС, якщо профіль користувача відповідає умовам."));

        foreach (var rule in _kb.Rules)
        {
            var conditions = string.Join(", ", rule.Conditions.Select(c => $"{TranslateField(c.Key)} = {TranslateChoice(c.Key, c.Value)}"));
            var optionName = _kb.Options.First(o => o.Id == rule.OptionId).Name;

            panel.Children.Add(new Border
            {
                CornerRadius = new CornerRadius(20),
                Padding = new Thickness(18),
                Background = UiBrushes.Card,
                BorderBrush = UiBrushes.Brush(115, 255, 255, 255),
                BorderThickness = new Thickness(1),
                Child = new StackPanel
                {
                    Spacing = 7,
                    Children =
                    {
                        new TextBlock { Text = $"{rule.Id}. {rule.Title}", FontSize = 18, FontWeight = FontWeights.Bold, Foreground = UiBrushes.CardText },
                        new TextBlock { Text = $"Рішення: {optionName}; вага: {rule.Weight}", Foreground = UiBrushes.Brush(37, 99, 235) },
                        new TextBlock { Text = $"Умови: {conditions}", TextWrapping = TextWrapping.Wrap, Foreground = UiBrushes.CardMuted },
                        new TextBlock { Text = rule.Explanation, TextWrapping = TextWrapping.Wrap, Foreground = UiBrushes.CardText }
                    }
                }
            });
        }
    }

    private UIElement CreateSectionHeader(string title, string subtitle)
    {
        return new Border
        {
            CornerRadius = new CornerRadius(22),
            Padding = new Thickness(20),
            Background = UiBrushes.SectionHeader,
            BorderBrush = UiBrushes.SectionHeaderBorder,
            BorderThickness = new Thickness(1),
            Child = new StackPanel
            {
                Spacing = 4,
                Children =
                {
                    new TextBlock { Text = title, Foreground = UiBrushes.White, FontSize = 20, FontWeight = FontWeights.Bold },
                    new TextBlock { Text = subtitle, Foreground = UiBrushes.Muted, TextWrapping = TextWrapping.Wrap }
                }
            }
        };
    }

    private string TranslateField(string field) => field switch
    {
        nameof(UserProfile.DeviceType) => "тип пристрою",
        nameof(UserProfile.MainTask) => "основна задача",
        nameof(UserProfile.HardwareLevel) => "рівень обладнання",
        nameof(UserProfile.UserExperience) => "досвід користувача",
        nameof(UserProfile.SoftwareCompatibility) => "сумісність",
        nameof(UserProfile.SecurityPriority) => "безпека",
        nameof(UserProfile.Administration) => "адміністрування",
        nameof(UserProfile.Budget) => "бюджет",
        _ => field
    };

    private string TranslateChoice(string field, string value)
    {
        var source = field switch
        {
            nameof(UserProfile.DeviceType) => _kb.DeviceTypes,
            nameof(UserProfile.MainTask) => _kb.MainTasks,
            nameof(UserProfile.HardwareLevel) => _kb.HardwareLevels,
            nameof(UserProfile.UserExperience) => _kb.UserExperience,
            nameof(UserProfile.SoftwareCompatibility) => _kb.Compatibility,
            nameof(UserProfile.SecurityPriority) => _kb.SecurityPriorities,
            nameof(UserProfile.Administration) => _kb.Administration,
            nameof(UserProfile.Budget) => _kb.Budgets,
            _ => []
        };

        return source.FirstOrDefault(item => item.Id == value)?.Title ?? value;
    }
}
