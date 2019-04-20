namespace cinefilo.Interfaces {
    using System.IO;

    public interface IShare {
        void ShareImage(string subject, string filename, byte[] data);
        void DeleteFiles();
        byte[] ConvertImageStream(string content);
    }
}
