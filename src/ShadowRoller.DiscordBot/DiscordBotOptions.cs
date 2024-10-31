namespace ShadowRoller.DiscordBot;
internal class DiscordBotOptions
{
    public const string SECTIONNAME = "DiscordBot";
    public required string Token { get; set; }
    public required string Prefix { get; set; }
    public required string Delimiter { get; set; }
}
