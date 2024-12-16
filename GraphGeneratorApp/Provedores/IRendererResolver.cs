namespace GraphGeneratorApp.Provedores
{
    public interface IRendererResolver
    {
        object GetRenderer(VisualElement element);

        bool HasRenderer(VisualElement element);
    }
}