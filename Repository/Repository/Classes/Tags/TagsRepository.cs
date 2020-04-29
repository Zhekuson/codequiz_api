using Domain.Models.Tags;
using Repository.Repository.Interfaces.Tags;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes.Tags
{
    public class TagsRepository : EntityRepository, ITagsRepository
    {
        public TagsRepository()
        {

        }
        public async Task<IEnumerable<TagCountPair>> GetTagCountPairs()
        {
            return await ExecuteQueryGetTagCountPairs();
        }
        [QueryExecutor]
        public async Task<IEnumerable<TagCountPair>> ExecuteQueryGetTagCountPairs()
        {
            List<TagCountPair> tagCountPairs = new List<TagCountPair>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                List<Tag> tags = new List<Tag>();
                SqlCommand command = CreateCommand($"SELECT * FROM Tag", connection);
                using (SqlDataReader sqlDataReader = command.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Tag tag = new Tag();
                            tag.ID = sqlDataReader.GetInt32ByName("id");
                            tag.Name = sqlDataReader.GetStringByName("tag_name");
                            tags.Add(tag);
                        }
                    }
                }
                foreach (Tag tag in tags)
                {
                    command = CreateCommand($"SELECT COUNT(*) AS CNT FROM Tag " +
                    " JOIN QuestionTag ON QuestionTag.tag_id = Tag.id " +
                    $" JOIN Question ON Question.id = QuestionTag.question_id WHERE tag_name = '{tag.Name}'", connection);
                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            sqlDataReader.Read();
                            TagCountPair tagCountPair = new TagCountPair();
                            tagCountPair.Tag = tag;
                            tagCountPair.Count = sqlDataReader.GetInt32ByName("CNT");
                            tagCountPairs.Add(tagCountPair);
                        }
                    }
                }

            }
            return tagCountPairs;
        }
    }
}
