using Canvas.Models;

namespace Canvas.Services {

    public class ContentItemService {
        private static ContentItemService instance;
        public static ContentItemService Current {
            get {
                if (instance == null) {
                    instance = new ContentItemService();
                }
                return instance;
            }
        }

        private IList<ContentItem> contentItems;

        public IEnumerable<ContentItem> ContentItems
        {
            get
            {
                return contentItems;
            }
        }

        private ContentItemService() {
            contentItems = new List<ContentItem>();
        }

        public IEnumerable<ContentItem> GetByCourse(Guid courseId) {
            return contentItems.Where(c => c.CourseId == courseId);
        }

        public void Add(ContentItem contentItem)
        {
            contentItems.Add(contentItem);
        }
    }
}